using ConsoleApp1;
using ConsoleApp1.MenuItems;

var application = new Application();

application.AddMenuItem(new MenuItemCreate(1, "Добавить встречу"));
application.AddMenuItem(new MenuItemShowAll(2, "Показать список встреч"));
application.AddMenuItem(new MenuItemEdit(3, "Редактировать"));
application.AddMenuItem(new MenuItemDelete(4, "Удалить встречу"));
application.AddMenuItem(new MenuItemSave(5, "Сохранить в текстовый файл"));
application.AddMenuItem(new MenuItemClose(6, "Завершение работы"));
         
application.Run();