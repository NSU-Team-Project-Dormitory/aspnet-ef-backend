### Порядок установки и запуска

1. Скачайте и установите Docker Desktop с [официального сайта](https://docs.docker.com/get-docker/)
2. Скачайте и установите Node.js с [официального сайта](https://nodejs.org/en)
3. Перейдите в удобную для вас директорию
4. Клонируйте репозитории с бэкэндом и фронтендом следующими способами:
   1. Бэкэнд-часть
      - http: `https://github.com/NSU-Team-Project-Dormitory/aspnet-ef-backend.git`
      - ssh: `git@github.com:NSU-Team-Project-Dormitory/aspnet-ef-backend.git`
   2. Фронтенд-часть
      - http: `https://github.com/NSU-Team-Project-Dormitory/react-frontend.git`
      - ssh: `git@github.com:NSU-Team-Project-Dormitory/react-frontend.git`
5. Перейдите в папку с бэкэндом командой `cd aspnet-ef-backend`
6. Выполните сборку и запуск docker контейнеров: `docker-compose up --build`
7. Дождитесь окончания запуска
8. Перейдите в папку с фронтендом `cd react-frontend`
9. Установите все необходимые библиотеки командой `npm install`
10. Запустите react приложение `npm start`
11. Приятного пользования