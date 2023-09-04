# DotNet Blazor Example

This is an example project using .NET Core and Blazor.

## Table of Contents

- [DotNet Blazor Example](#dotnet-blazor-example)
  - [Table of Contents](#table-of-contents)
  - [Overview](#overview)
- [Project Structure](#project-structure)
  - [Installation](#installation)
- [Example installation steps](#example-installation-steps)

## Overview
This solution consists of two main components: a server and a client. The server encompasses an API developed using .NET Core, while the client is developed using Blazor. These two components are designed to work together seamlessly through API communication.

# Project Structure

- DotNetBlazor (Solution folder)
  - DotNetBlazor.Server (Server project)
  - DotNetBlazor.Client (Client project)
  - DotNetBlazor.Shared (Shared Library for both project)
  - DotNetBlazor.Server.Test (Test project for server)

## Installation

```bash
# Example installation steps
1. Clone the repository: `git clone https://github.com/uttamraz/dotnet-blazor.git`
2. Navigate to the project directory: `cd your-repo`
3. Install dependencies: `dotnet restore`
4. Configure the server and client: Update the respective configuration files.
5. Run the solution: `dotnet run` or use Visual Studio or VS Code to build and run.
