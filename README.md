# MS-Logging
[![Build Status](https://dev.azure.com/sofisofttech/Transversales/_apis/build/status/MS-Logging?branchName=main)](https://dev.azure.com/sofisofttech/Transversales/_build/latest?definitionId=1&branchName=main)
[![Deploy Status](https://vsrm.dev.azure.com/sofisofttech/_apis/public/Release/badge/ef81836a-f33c-4048-8131-535d3fe35113/1/1)](https://vsrm.dev.azure.com/sofisofttech/_apis/public/Release/badge/ef81836a-f33c-4048-8131-535d3fe35113/1/1)
[![Image Version](https://img.shields.io/docker/v/sofisoft/loggingapi/latest?logo=docker)](https://hub.docker.com/r/sofisoft/loggingapi)
[![Image Size](https://img.shields.io/docker/image-size/sofisoft/loggingapi/latest?logo=docker)](https://hub.docker.com/r/sofisoft/loggingapi)
[![Security Headers](https://img.shields.io/security-headers?logo=openssl&url=https%3A%2F%2Fservices.sofisoft.pe%2Flogging)](https://securityheaders.com/?q=https%3A%2F%2Fservices.sofisoft.pe%2Flogging&followRedirects=on)
[![Swagger](https://img.shields.io/swagger/valid/3.0?logo=swagger&specUrl=https%3A%2F%2Fservices.sofisoft.pe%2Flogging%2Fswagger%2Fv1%2Fswagger.json)](https://services.sofisoft.pe/logging)
[![Status Api](https://img.shields.io/website?url=https%3A%2F%2Fservices.sofisoft.pe%2Flogging%2Findex.html)](https://services.sofisoft.pe/logging/index.html)

Microservicio para el registro de eventos.

## Inicio
Siga las siguientes instrucciones para ejecutar el proyecto

### Pre-Requisitos

#### Configuracion de .Net core:

* Descargar la versión 5.0 del SDK segun el sistema operativo ([lista de SDK](https://dotnet.microsoft.com/download))

* Seguir los pasos de la instalación

#### Instrucciones para levantar el api

* Verificar la cadena de conexión en appsettings.Development.json hacia a MongoDB en la nube

* Ejecutar el siguiente comando:
    ```jshelllanguage
    $ dotnet run
    ```
El proyecto se ejecutar por defecto en la siguiente url:

[http://localhost:5000/index.html](http://localhost:5000/index.html)

***

## Tecnologías utilizadas
[![Net Core](https://img.shields.io/badge/net%20core-v5.0-blue?logo=.net)](https://dotnet.microsoft.com/download)
[![MongoDB](https://img.shields.io/badge/mongodb-nosql-green?logo=mongodb)](https://www.mongodb.com/es)
[![Docker](https://img.shields.io/badge/docker-container-blue?logo=docker)](https://www.docker.com/)
[![Microsoft Azure](https://img.shields.io/badge/azure-cloud-blue?logo=microsoft-azure)](https://azure.microsoft.com/)

## Sofisoft Technologies

![Sofisoft](https://files.sofisofttech.com/public/images/sofisoft-technologies.png)
