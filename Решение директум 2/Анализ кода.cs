// Для работы с XML документами есть специальные пространства имен

// Название функции должно описывать что эта функция делает
static string Func1(string input, string elementName, string attrName) 
{
  // Отсутствует проверка переданных параметров на null и пустую строку
  // Стоит добавить проверку на существование файла по указанному пути
  // Пользователь ожидает в качестве результат работы функции строку, лучше вернуть пустую строку чем null 
  // string result = string.Empty  
  string[] lines = System.IO.File.ReadAllLines(input);
  string result = null;
 
  foreach (var line in lines)
  {
    // Может быть найден индекс элемента/значения название которого начинается на elementName   
    var startElEndex = line.IndexOf(elementName);
 
    if (startElEndex != -1)
    {
      if (line[startElEndex - 1] == '<')
      {
        var endElIndex = line.IndexOf('>', startElEndex - 1);
        var attrStartIndex = line.IndexOf(attrName, startElEndex, endElIndex - startElEndex + 1);
 
        if (attrStartIndex != -1)
        {
          // Не учитваются в каком виде записан атрибут
          // Атрибут и его знчение могут быть записаны в формате attrName = "attrValue" // attrName='attrName="blabla"'
          
          int valueStartIndex = attrStartIndex + attrName.Length + 2; 
          
          while (line[valueStartIndex] != '"')
          {
            // Т.к.строки это неизменяемый тип то в данном месте возможно создание большого кол-во новых объектов 
            // из-за чего будет часто производиться сборка мусора 
            // что может негативно сказаться на скорости выполнения программы
            result += line[valueStartIndex]; 
            valueStartIndex++;
          }
          // Возвращается значение первого найденного элемента
          break;
        }
      }
    }
  }
 
  return result;
}



// Элементов с переданным атрибудтом может быть несколько, поэтому возвращаем IEnumerable<string>
// Вызывающий код сам решит что делать, брать первое значение или использовать все

using using System.Xml.Linq;

static IEnumerable<string> GetAttrValues(string input, string elementName, string attrName)
{
    if (string.IsNullOrEmpty(input))
    {
        throw new ArgumentNullException($"{nameof(input)} is null or empty");
    }
    if (string.IsNullOrEmpty(elementName))
    {
        throw new ArgumentNullException($"{nameof(elementName)} is null or empty");
    }
    if (string.IsNullOrEmpty(attrName))
    {
        throw new ArgumentNullException($"{nameof(attrName)} is null or empty");
    }

    var root = XElement.Load(input);

    var result = from el in root.Elements(elementName)
                 select el.Attribute(attrName)?.Value;

    return result;
}