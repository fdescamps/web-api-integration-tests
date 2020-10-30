namespace Zoo.Api.Routing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Microsoft.AspNetCore.Mvc.ApplicationParts;
    using Microsoft.AspNetCore.Mvc.Controllers;

    internal class GenericControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        private readonly Type controllerType;

        public GenericControllerFeatureProvider(Type controllerType)
        {
            this.controllerType = controllerType;
        }

        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            var genericArguments = this.controllerType.GetGenericArguments();

            var baseTypes = genericArguments.Select(type => type.BaseType ?? type).ToList();
            var groups = baseTypes.Select(
                                      baseType =>
                                          {
                                              var assembly = baseType.Assembly;
                                              var types = assembly.GetExportedTypes().Where(
                                                                      type => type.IsSubclassOf(baseType))
                                                                  .Select((type, index) => (Index: index, Type: type))
                                                                  .ToList();
                                              return types;
                                          })
                                  .SelectMany(item => item)
                                  .GroupBy(item => item.Index, item => item.Type)
                                  .Where(g => g.Count() == 3);

            foreach (var group in groups)
            {
                var controllerInfo = this.controllerType.MakeGenericType(group.ToArray()).GetTypeInfo();
                feature.Controllers.Add(controllerInfo);
            }
        }
    }
}