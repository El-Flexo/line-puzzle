# Lines puzzle

## Исходная задача:
* 4 разных цвета камешков;
* начисление очков в зависимости от длины цепочки;
* минимальная длина цепочки 4 камешка;
* ограниченное количество цепочек в рамках одной сессии.

Аналог - [Jelly Splash](https://apps.facebook.com/jellysplash)

## Unit тесты
* код тестов и ресурсы к ним находятся в `line-puzzle/tests`
* тесты охватывают большую часть модели геймплея
* под mac os тесты можно запустить скриптом `line-puzzle/tests/run.sh` (необходим установленный mono)

## Примечания
* в проекте использован фрэймворк [Strange IOC](http://strangeioc.github.io/strangeioc/) (MVCS + DI)
* модели основного геймплея находятся в пакете `matchPuzzle/MVCS/model/core`
* представление игры находится в пакете `matchPuzzle/MVCS/view/game`
* приложение спроектировано с учетом возможности добавления функциональности такой как:

>* использование бандлов (настройки уровней вынесены вайлы описания, используются префабы уровней)
>* реализации туториала (генератор элементов вынесен в отдельный интерфейс, в модели используется ГПСЧ)
>* переход на разделяемую логику приложения (модель полностью инкапсулирована от реализации)
>* портирование на мобильные устройства (инпут реализван отдельным компонентом)

* в приложении использована графика из пака [Kenney Game Assets](http://kenney.itch.io/kenney-donation)
* процесс планирования и примерный лог разработки можно посмотреть [здесь](https://trello.com/b/UiwvrlwQ/line-puzzle)

## Другие пояснения
* целевая версия Unity - 4.6.3
