using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace NotifyMaster.Common.Helpers;

public static class EnumHelper
{
    public static string ToDisplayText(this Enum targetEnum)
    {
        Type type = targetEnum.GetType();
        MemberInfo[] memInfo = type.GetMember(targetEnum.ToString());

        if (memInfo?.Length > 0)
        {
            object[] attrs = memInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false);

            if (attrs?.Length > 0)
            {
                return ((DisplayAttribute)attrs[0]).Name;
            }
        }
        return targetEnum.ToString();
    }
}
