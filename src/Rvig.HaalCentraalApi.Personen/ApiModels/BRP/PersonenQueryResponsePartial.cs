
using System.Reflection;

namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP
{
    public partial class PersonenQueryResponse
    {
        public static PersonenQueryResponse? MapFrom(Deprecated.PersonenQueryResponse response)
        {
            if (response == null) return null;

            var sourceType = response.GetType();
            var targetTypeName = sourceType.Name.Replace("Deprecated.", string.Empty);
            var targetType = typeof(PersonenQueryResponse).Assembly.GetTypes()
                .FirstOrDefault(t => t.Name == targetTypeName);

            if (targetType == null)
                throw new InvalidOperationException($"No matching target type found for {sourceType.Name}");

            var result = Activator.CreateInstance(targetType);

            var sourceProps = sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var targetProps = targetType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var sourceProp in sourceProps)
            {
                if (string.Equals(sourceProp.Name, "gezag", StringComparison.OrdinalIgnoreCase))
                    continue;

                var targetProp = targetProps.FirstOrDefault(p => p.Name == sourceProp.Name && p.PropertyType == sourceProp.PropertyType && p.CanWrite);
                if (targetProp != null)
                {
                    var value = sourceProp.GetValue(response);
                    targetProp.SetValue(result, value);
                }
            }

            return (PersonenQueryResponse)result;
        }
    }
}