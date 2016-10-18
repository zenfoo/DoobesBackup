# Doobes Backup Service
Manage backup synchronisation across various sources and destinations

Self-hosted .NET Core backend service using the NanxyFx framework with an Angular 2 frontend UI. These are currently hosted independently but eventually intended to be fully self-contained.

## Build environment
* Built on Visual Studio Community 2015.3
* Requires .NET Core 1.0.1 SDK (1.0.0-preview2-003131)
* To host the Angular UI app in IIS with the supplied web.config, you will also need to install the IIS Url Rewrite 2.0 module which can be found here: https://www.iis.net/downloads/microsoft/url-rewrite