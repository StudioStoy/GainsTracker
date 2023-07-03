# GainsTracker CoreAPI

This is a guide on how to get started and explains the basic gist of this project

> This is part of the [**GainsTracker**](https://github.com/StudioStoy/GainsTracker) project.

___
**Table of Contents**

<div id="user-content-toc">
  <ul>
    <li><a href="#1. Getting Ready">1-Getting-ready</a>
      <ul>
        <li><a href="#1.1 Prerequisites">11-prerequisites</a></li>
        <li><a href="#1.2 Installers">12-installers</a></li>
        <li><a href="#1.3 Setting up the project and environments">13-setting-up-the-project-and-environments</a></li>
      </ul>
    </li>
  </ul>
</div>

___
## 1. Getting Ready

### 1.1 Prerequisites
Make sure you have the following tools installed: 
- Docker
- dotnet ef CLI 
- Makefile (Optional for running and compiling easily, see chapter 1.2)

### 1.2 Installers
In the folder `/Gainstracker.CoreAPI/Installers`, two installers reside. One for windows (.bat) and one for linux (.sh).
For now, these basically perform two functions:
- When run (from the command line or outside of that), they check and install [makefile](https://opensource.com/article/18/8/what-how-makefile) if necessary.
- If your system has no way to install it, it will install a command line package manager for your system (on windows, that'll be [Chocolatey](https://chocolatey.org/)).

You can also always install these tools manually.

### 1.3 Setting up the project and environments

Righto, you have pulled the project and are in the `/Gainstracker.CoreAPI` directory.
Time to get out of there! To keep things simple, most commands can be done from the root of the project.

Back at the root directory, there are a couple of different running environments:

- **Local** (`make local`): this is the local environment. 
  - This sets up a postgresql database. You can then run the application to test with this database (reuse or reset db profile).
- **Docker** (`make docker`): this is the fully dockerized environment.
  - This sets up a postgresql database and builds and launches the CoreAPI project.
    This means it will run like a fully detached application with a database.
- **Staging** (`make staging`): this is the staging environment for testing.
  - this builds and starts the application that connects to 
    to the staging database.
- **Production** (`make production`): this is the production environment that will actually run in production.
  - this builds and starts the application that connects to
    to the production database, and runs all code specifically for the production environment.

> If you don't (want to) have the makefile tool installed, you can use the docker-compose files and commands to do the same. 





© [StudioStoy 2023](https://studiostoy.nl)
