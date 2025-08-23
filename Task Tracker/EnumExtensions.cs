using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Task_Tracker
{
  public static class EnumExtensions
  {
    public static string GetDescription<T>(this T enumValue) where T : Enum
    {
      var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

      var attribute = fieldInfo?
          .GetCustomAttribute<DescriptionAttribute>(false);

      return attribute?.Description ?? enumValue.ToString();
    }
  }
}
