using RecruitmentDomain.Models;
using System.Reflection;

namespace RecruitmentCore.Common
{
    public static class EnumExtensions
    {
        public static string ResponseCode(this Enum value)
        {
            string result = value.ToString("D").PadLeft(2, '0');
            return result;
        }

        public static string DisplayName(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            MetaData attribute = Attribute.GetCustomAttribute(field, typeof(MetaData)) as MetaData;

            return attribute == null ? value.ToString() : attribute.Name;
        }

        public static string Description(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            MetaData attribute = Attribute.GetCustomAttribute(field, typeof(MetaData)) as MetaData;

            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static GenericResponse<List<MetaDataResponse>> GetEnumList<T>() where T : Enum
        {
            var result = Enum.GetValues(typeof(T));
            List<MetaDataResponse> list = new();

            foreach (int item in result)
            {
                var resultItem = Enum.GetName(typeof(T), item);

                MetaDataResponse data = new()
                {
                    Code = item,
                    Description = resultItem
                };
                list.Add(data);
            }
            return GenericResponse<List<MetaDataResponse>>.Success(list, "");
        }
    }

    public class MetaData : Attribute
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
