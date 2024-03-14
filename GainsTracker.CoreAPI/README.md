# GainsTracker CoreAPI

This is a guide on how to get started and explains the basic gist of this project.

> This is part of the [**GainsTracker**](https://github.com/StudioStoy/GainsTracker) project.

___
**Table of Contents**

<div id="user-content-toc">
  <ul>
    <li><a href="#1. Getting Ready">1. Getting Ready</a>
      <ul>
        <li><a href="#11-prerequisites">1.1 Prerequisites</a></li>
        <li><a href="#12-(Optional)-Installers-for-easier-setup">1.2 Installers</a></li>
        <li><a href="#13-.env">1.3 .env</a></li>
      </ul>
    </li>
    <li><a href="#2. Up and Running">2. Up and Running</a>
    </li>
    <li><a href="#3. Misc">3. Misc</a>
      <ul>
        <li><a href="#31-environments">1.3 Environments</a></li>
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

### 1.2 (Optional) Installers for easier setup

In the folder `/Gainstracker.CoreAPI/Installers`, two installers reside. One for windows (.bat) and one for linux (.sh).
For now, these basically perform two functions:

- When run (from the command line or outside of that), they check and
  install [makefile](https://opensource.com/article/18/8/what-how-makefile) if necessary.
- If your system has no way to install it, it will install a command line package manager for your system (on windows,
  that'll be [Chocolatey](https://chocolatey.org/)).

You can also always install these tools manually.

### 1.3 .env

You can use a `.env` file that stores the development password used by the database and set the database connection
strings.

---

## 2. Up and Running

To get the application up and running with the database in Docker, you first gotta make sure Docker is running. Then,
open up a
terminal in the `<root>/Gainstracker.CoreAPI` directory, and run the `make local` commando (see chapter 3.1)
or `docker compose up`.

When that is running, it's time for database migrations!!! Yippie!! Anyhow, if there are already migrations in
the `Migrations` folder, you can skip to command 2.

1. Run `dotnet ef migrations add *01_NameOfYourMigration*`.
2. Run `dotnet ef database update`.

This will make sure the PostgreSQL database schema running in the Docker container is up-to-date.

Cool, you should be **Up 'n Running** now!'

---
## 3. Misc

### 3.1 Environments

Righto, you have definitely read all of that and are now in the `/Gainstracker.CoreAPI` directory.
Time to get out of there! To keep things simple, most commands can be done from the root of the project.

Back at the root directory, there are a couple of different running environments:

- **Local** (`make local`): this is the local environment.
    - This sets up a postgresql database. You can then run the application to test with this database (reuse or reset db
      profile).
- **Docker** (`make docker`): this is the fully dockerized environment.
    - This sets up a postgresql database and builds and launches the CoreAPI project.
      This means it will run like a fully detached application with a database.
- **Staging** (`make staging`): this is the staging environment for testing.
    - this builds and starts the application that connects to
      to the staging database.
- **Production** (`make production`): this is the production environment that will actually run in production.
    - this builds and starts the application that connects to
      to the production database, and runs all code specifically for the production environment.

> If you don't (want to) have the makefile tool installed, you can use the docker-compose files and commands to do the
> same.

---
© [StudioStoy 2024](https://studiostoy.nl)
