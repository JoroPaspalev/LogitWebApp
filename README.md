# ASP.NET 5.0  :v: **Logit - Transport Company**

## :point_right: Project Introduction :point_left:

**Logit - site for transport company** is my defense project for **ASP.NET 5.0** course at [SoftUni](https://softuni.bg/trainings/3177/asp-dot-net-core-october-2020/internal#lesson-18363). 
It is a ready-to-use ASP.NET 5.0 application.

## :pencil: Описание на проекта
Уеб приложението предоставя модерен графичен интерфейс за работа. Комбинира в себе си много функционалност, която може да бъде полезна за потребителите. В горната му част като повечето уеб приложения има удобен навигационен панел със следните менюта: “Начало”, “За нас”, “Регистрация”, “Вход”, “Контакти”. В зависимост от ролята на всеки вписан потребител се променят и връзките за достъп до определените ресурси, успоредно с това и страниците които да се показват.
В дъното на всяка страница има footer или мястото, където може да намерите препратка към останалите страници от системата – “Начало”, “За нас”, “Регистрация”, “Вход”, “Контакти”.
Нека започнем с описание на всяка една от страниците.

**_Описание на начална страница “Начало”:_**
На заглавната страница има форма, чрез която потребителя може да получи оферта за превоз на палети между посочените градове в падащото меню, без да е нужна регистрация в сайта. 
Ако офертата го устройва, следва да се извърши регистрация, за да може да продължи към поръчка. 
След успешно извършена регистрация, потребителя следва да въведе адрес на товарене и адрес на разтоварване на стоката, както и данни за връзка с адресата и адресанта, допълнени с дати за товарене и разтоварване. След правилното въвеждане на всички данни и изпращане на поръчката, същата може да бъде видяна в навигационния панел в меню "Моите поръчки", където се показва информация за всяка една направена поръчка от дадения потребител. 
Освен данни за поръчката, юзъра може да види, кой шофьор превозва стоката и да го оцени в зависимост от неговото представяне. 
Има и две допълнителни функционалности - първата, е че има възможност да се принтира .pdf на самата поръчка, а втората да се разгледат снимки направени от шофьора, при товарене и разтоварване на палетите (Дали са добре подредени в камиона, укрепени с колани и т.н.), както и при възникване на ситуации от застрахователни претенции. 

**_Описание на страница “За нас”:_**
Тук можете да намирате информация за извършваната дейност от фирмата.

**_Описание на страница “Регистрация”:_**
Това е частта, където потребителя се регистрира.

**_Описание на страница “Вход”:_**
Страницата за вход в сайта.

**_Описание на страница “Контакти”:_**
Тук можете да намирате информация, как да се свържете с фирмата - адрес, телефон, email и работно време. 

**_Описание на страница “Моите поръчки”:_**
В тази страница се листват всички поръчки направени от даден потребител, като последно направената се показва най-отгоре. След кликане на съответната поръчка се отваря щора, която съдържа цялата налична информация за пратката.
Освен данни за поръчката, юзъра може да види, кой шофьор превозва стоката и да го оцени в зависимост от неговото представяне. Има и две допълнителни функционалности - първата, е че има възможност да се принтира .pdf на самата поръчка, а втората да се разгледат снимки направени от шофьора, при товарене и разтоварване на палетите (Дали са добре подредени в камиона, укрепени с колани и т.н.), както и при възникване на ситуации от застрахователни претенции. 
Също така на страницата има изградено номериране, благодарение на което потребителя може да прелиства между отделните поръчки, показвайки по 6 на страница, а не всички те да бъдат листнати на един екран.

**_Описание на страница “Чат”:_**
Чрез интегрираната система за Чат, потребителите могат да си пишат с представител на фирмата и да получат писмено отговор на техните въпроси.

**_Описание на страница “Пратки на шофьора”:_**
В тази страница юзърите, (логнати с роля Driver) могат да си изберат пратка за превозване - от "Всички пратки" или да прегледат всички техни пратки - от "Мои пратки", които са приели за превозване.
Шофьорите имат възможност да променят всяка тяхна пратка - чрез бутона "Редактирай", като възможностите за корекция свързани със самата пратка са само върху габаритните размери, броя палети и теглото. 
При отбелязване, че стоката е доставена, тази опция става неактивна - успоредно с това клиента вижда статуса, без да е нужно да се обажда във фирмата за допълнителна информация
Чрез бутона "Прикачи снимка" - шофьора качка направените от него снички в базата данни. 

**_Описание на потребителския профил:_**
За потребителския профил е използвана стандартната функционалност, която се предоставя от ASP.NET Core Identity, разширена с допълнителни свойства, според нуждите на проекта.

**_Допълнителни функционалности:_**
Интегриран е Facebook login, който може да бъде използван вместо стандартната регистрация.
На лице е и "Scroll to top" бутон, който улеснява потребителя, връщайки го в началото на страницата, без да е нужно да скролира.
При получаване на ново съобщение в чата се издава звуков сигнал.

**_Заключение:_**
Logit е проект, който обединява на едно място удобен потребителски интерфейс, възможност за моментално получаване на оферта и лесното и преобразуване в поръчка.
 
## Unit tests Code coverage

![Code coverage](https://app.box.com/s/67wa4uuxsdran3hduq9sklj68gul40hb)

## :hammer: Used technologies
* ASP.NET [5.0](https://dotnet.microsoft.com/download/dotnet/5.0)
* ASP.NET Core areas
* Entity Framework [CORE 5.0.0](https://docs.microsoft.com/en-us/ef/core/what-is-new/ef-core-5.0/whatsnew)*
* [Bootstrap](https://github.com/twbs/bootstrap)
* JavaScript and JS animations
* [Facebook for developers](https://developers.facebook.com)
* [xUnit](https://github.com/xunit/xunit)
* In-Memmory Cache

## :floppy_disk: Database Diagram
![](https://app.box.com/s/rbm5rjsf9pvliemgdw7gt0tk4h6p4teg)

# Link
https://logitruse.azurewebsites.net/

## Author

[Joro Paspalev](https://github.com/JoroPaspalev?tab=repositories)
- Facebook: [@Joro Paspalev](https://www.facebook.com/joro.paspalev.7/)
- LinkedIn: [@Joro Paspalev](https://www.linkedin.com/in/joro-paspalev-a91bab186/)

## Template
- ASP.NET MVC 5

## :v: Show your opinion

Give a :star: if you like this project!
