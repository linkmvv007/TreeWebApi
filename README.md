﻿# TreesTestTask

# !!! при первом запуске приложения выполняются миграции по созданию необходимых таблиц и индексов
# В appsettings.json указана строка подключения к БД Postgres

# PS:
# Сказали делать как в сваггере описано, поэтому все методы API - это POST запросы

# В виду этого у меня есть следующие замечания:
# - методы по работе с узлами дерева (rename, delete, create)
#  на мой взгляд содержат избыточный параметр treeName. Он добавлен, проверяет дополнительно, что узел принадлежит указанному дереву
# - все методы в API типа POST с кодом возврата 200, хотя это не соответствует стандарту HTTP.
# Например, POST используется для создания объекта с кодом возврата 201.
# 		  PUT - для обновления(переименования) объекта с кодом 200
# 		  DELETE - для удаления объекта c кодом 200 или 204
# - методы получения данных , ка кправило, get - запрос
