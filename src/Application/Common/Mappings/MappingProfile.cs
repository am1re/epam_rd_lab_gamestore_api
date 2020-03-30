using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using AutoMapper.Configuration.Internal;

namespace Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();
            
            foreach (var type in types)
            {
                var methodInfo = type.GetMethod("Mapping");
                var methodInfoFromInterface = (type.GetInheritedMember("Mapping").MemberType == MemberTypes.Method)
                    ? (MethodInfo) type.GetInheritedMember("Mapping")
                    : null;
                methodInfo ??= methodInfoFromInterface;
             
                var instance = Activator.CreateInstance(type);
                methodInfo?.Invoke(instance, new object[] {this});
            }
        }
    }
}