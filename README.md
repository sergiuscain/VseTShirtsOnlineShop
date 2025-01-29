<!DOCTYPE html>
<html lang="ru">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Онлайн магазин одежды</title>
</head>
<body>
    <h1>Онлайн магазин одежды</h1>
    <p>(В процессе написания и разработки!)</p>

    <h1>Используемые технологии:</h1>
    <ul>
        <li>
            <h4>Базы данных:</h4>
            <ul>
                <li>Entity Framework</li>
                <li>MsSql</li>
                <li>PostgreSql (через EF на неё переключался, потом вернул обратно на MsSQL)</li>
                <li>LINQ</li>
                <li>Миграции</li>
            </ul>
        </li>
        <li>
            <h4>Web:</h4>
            <ul>
                <li>ASP.NET</li>
                <li>MVC</li>
                <li>Частичные представления и компоненты представлений</li>
                <li>HTML</li>
                <li>Bootstrap</li>
            </ul>
        </li>
        <li>
            <h4>Прочее:</h4>
            <ul>
                <li>Работа с ООП</li>
                <li>Асинхронное программирование</li>
                <li>Dependency Injection</li>
                <li>Области area</li>
                <li>Работа с файловой системой</li>
                <li>Маппинг и собственный автомаппер через рефлексию</li>
                <li>C# и .Net</li>
            </ul>
        </li>
    </ul>

    <h4>Проект онлайн-магазина одежды. В проекте реализован основной функционал онлайн-магазина, необходимый для совершения покупок пользователями (выбор товара, поиск по каталогу, добавление в корзину/избранное/к сравнению, оформление заказа), реализован механизм авторизации и регистрации при помощи встроенной в ASP.NET Core технологии Identity (а ещё с помощью Яндекс и Google), а также панель администратора, позволяющая пользователю в роли администратора редактировать данные заказов, пользователей, товаров на сайте. Все данные заносятся и хранятся в базе данных MS SQL. При разработке приложения использовался подход CodeFirst.</h4>

    <h2>Обновления:</h2>
    <h3>05.01.25</h3>
    <p>Добавил авторизацию и регистрацию через Google и Yandex, но почему-то не могу залить в удаленный репозиторий. Сделал пару коммитов, но изменений достаточно в них. Пытаюсь разобраться, что не так с гитом.</p>

    <h3>04.01.25</h3>
    <p>Добавил пагинацию</p>
    <img src="https://github.com/user-attachments/assets/ef9baa53-d4e5-4376-92c4-7aa45a862617" alt="Пагинация"/>
    <img src="https://github.com/user-attachments/assets/74c14311-cf31-4400-94df-6db6ffe9cdd6" alt="Пагинация"/>

    <h1>Функционал сайта:</h1>
    <h3>1) Главная страница: на ней находится весь список товаров.</h3>
    <img src="https://github.com/user-attachments/assets/2cf88a1f-0ed9-4ef4-9a91-2dc06146f27c" alt="Главная страница"/>
    <h4>На главной странице есть фильтры и поиск. Поиск идёт по названию товара.</h4>
    <img src="https://github.com/user-attachments/assets/7a2aee20-d3c3-4e69-bd8f-c112ba1e9263" alt="Поиск"/>
    <h4>Список фильтров сделан в виде выпадающего меню. Можно фильтровать товары по диапазону цены и количеству на складе, а также по параметрам товара: «Категория», «Цвет», «Размер» и «Пол».</h4>
    <img src="https://github.com/user-attachments/assets/76f7fb35-d8fa-47a4-9d6f-9d1433a3e3ac" alt="Фильтры"/>
    <h4>Также на главной странице есть список коллекций (товары, объединенные по одной тематике). Управление коллекциями находится в меню администратора.</h4>
    <div>
        <img width="500" src="https://github.com/user-attachments/assets/4c607739-124d-4c76-bc33-16c1fe53790e" alt="Коллекция 1"/>
        <img width="500" src="https://github.com/user-attachments/assets/e93b729a-8396-4a93-a3d8-d1d18e53f7d6" alt="Коллекция 2"/>
    </div>
    <h4>Товары можно сравнить между собой.</h4>
    <img src="https://github.com/user-attachments/assets/f5f6e18a-17f4-4a00-8f3a-f9dd2bf5968c" alt="Сравнение товаров"/>
    <h4>Также, на главной странице и на странице сравнения товаров, можно перейти в карточку товара. В ней есть баннер с изображениями товаров, описания и возможность добавить в корзину.</h4>
    <img src="https://github.com/user-attachments/assets/5ee3143e-a3ec-4989-9d81-dac01df7bb7c" alt="Карточка товара"/>
    <img src="https://github.com/user-attachments/assets/58190c67-4800-4352-91e3-e17f833e728a" alt="Карточка товара 2"/>
    <img src="https://github.com/user-attachments/assets/1a818d9a-89b1-422d-a257-d5a47539672f" alt="Карточка товара 3"/>

    <h3>Избранное</h3>
    <h4>Во вкладке избранное отображаются товары авторизованного пользователя, которые он добавил туда.</h4>
    <h4>Есть возможность удалить из избранного, перейти в карточку товара (по названию кликнуть), сравнить и добавить в корзину.</h4>
    <h6>(Также в хэдере динамически обновляется счётчик товаров в избранном)</h6>
    <img src="https://github.com/user-attachments/assets/e3cf4d39-00b4-4535-94fc-3273785211a1" alt="Избранное"/>

    <h1>Профиль пользователя</h1>
    <h4>Нажав на ник пользователя, мы переходим в личный кабинет (пока он не доделан немного). Доступно только авторизованному пользователю.</h4>
    <img src="https://github.com/user-attachments/assets/ef037a3b-bb57-47d9-a05b-4a38d394ab98" alt="Профиль пользователя"/>

    <h1>Авторизация и регистрация</h1>
    <h4>Выполнена с помощью ASP.NET Core Identity</h4>
    <img src="https://github.com/user-attachments/assets/fd6b80cf-b686-4b5f-b650-06ecb10e35ac" alt="Авторизация"/>
    <img src="https://github.com/user-attachments/assets/038a39af-2e41-4234-bf96-539ac727c41a" alt="Регистрация"/>
    <h4>Если авторизован пользователь без роли «админ», ему недоступна панель админа!</h4>
    <img src="https://github.com/user-attachments/assets/f0b2220d-3072-442b-8bd1-c9b99e1084f5" alt="Панель админа"/>

    <h1>Корзина</h1>
    <h4>В корзине идёт подсчёт стоимости каждой позиции товара. Также есть возможность изменять количество товаров в позициях (если оно будет равно нулю, позиция удалится. Если в корзине будет 0 позиций, корзина удалится).</h4>
    <img src="https://github.com/user-attachments/assets/af9398a9-015e-4a6e-849d-bcea691274d4" alt="Корзина 1"/>
    <img src="https://github.com/user-attachments/assets/86dd5f3c-8047-40b2-afd6-b050bb2a6bdd" alt="Корзина 2"/>
    <h6>Страницу оформления заказа надо бы переделать!</h6>
    <img src="https://github.com/user-attachments/assets/5493842b-3924-4822-a22d-0959aa3a679a" alt="Оформление заказа"/>

    <h1>Панель админа.</h1>
    <h4>Содержимое панели админа доступно только админам!</h4>
    <img src="https://github.com/user-attachments/assets/f0fb104a-a98c-46bc-8574-7cfed647b258" alt="Панель админа 1"/>
    <img src="https://github.com/user-attachments/assets/ce8ff1c7-fdc8-47b7-8456-831463c4036a" alt="Панель админа 2"/>
    <img src="https://github.com/user-attachments/assets/73be3244-52fc-4a40-836a-86f432c66984" alt="Панель админа 3"/>
    <img src="https://github.com/user-attachments/assets/a974c507-eb71-4b94-b7ab-b18f08d1c6e7" alt="Панель админа 4"/>
    <img src="https://github.com/user-attachments/assets/a51c3178-44c5-4f37-abbe-584171d776ac" alt="Панель админа 5"/>
    <h4>Возможность добавить рандомно 1, 10 или 50 товаров сделал для себя, чтобы в процессе разработки можно было быстро добавлять товары.</h4>
    <img src="https://github.com/user-attachments/assets/6734454f-e044-435d-a7b7-5d920dafda26" alt="Добавление товаров"/>

    <h3>Коллекции</h3>
    <img src="https://github.com/user-attachments/assets/72d53db6-782f-44d3-be29-ac18940c94a1" alt="Коллекции 1"/>
    <img src="https://github.com/user-attachments/assets/e9661122-42e2-4d01-ba3f-3575fda772a2" alt="Коллекции 2"/>
</body>
</html>
