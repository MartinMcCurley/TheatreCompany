# Theatre Company Website

## Description

A modern, responsive website for a Theatre Company built with ASP.NET Core MVC. The project features a clean design, robust backend, and various integrations for managing theatre productions, events, and customer interactions.

## Features

- 🎭 Theatre production showcase
- 📅 Event calendar and booking system
- 🎟️ Ticket management
- 👥 Member accounts and profiles
- 📱 Responsive design
- 🗺️ Google Maps integration
- 💬 Blog and comments system
- 📧 Contact form with email notifications

## Technologies

- **Backend**: ASP.NET Core 6.0
- **Frontend**: HTML5, CSS3, JavaScript
- **Database**: SQL Server
- **Authentication**: ASP.NET Core Identity
- **CSS Framework**: Bootstrap 5
- **JavaScript Libraries**: jQuery, Google Maps API
- **Package Management**: LibMan, NuGet

## Prerequisites

- .NET 6.0 SDK or later
- SQL Server 2019 or later
- Node.js (for development tools)
- Visual Studio 2022 or VS Code

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/TheatreCompany.git
   ```

2. Navigate to the project directory:
   ```bash
   cd TheatreCompany
   ```

3. Create a `.env` file based on `.env.example`:
   ```bash
   cp .env.example .env
   ```

4. Update the environment variables in `.env` with your settings

5. Restore dependencies:
   ```bash
   dotnet restore
   ```

6. Update the database:
   ```bash
   dotnet ef database update
   ```

7. Run the application:
   ```bash
   dotnet run
   ```

## Configuration

The application uses various configuration settings that can be set through environment variables or the `.env` file:

- Database connection string
- Google Maps API key
- SMTP settings for email
- Authentication settings
- Logging configuration

See the `.env` file for all available configuration options.

## Development

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Run tests
5. Submit a pull request

### Code Style

- Follow C# coding conventions
- Use meaningful variable and function names
- Add comments for complex logic
- Keep methods focused and concise

## Deployment

1. Update environment variables for production
2. Build the application:
   ```bash
   dotnet publish -c Release
   ```
3. Deploy to your hosting platform

## Contributing

1. Check the issues page for open tasks
2. Follow the development workflow
3. Ensure your code follows the project standards
4. Update documentation as needed

## Testing

Run the test suite:
```bash
dotnet test
```

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Credits

- Design inspiration: [Theatre Website Templates]
- Icons: Font Awesome
- Maps: Google Maps Platform
- Original development: [Your Name]

## Support

For support, please:
1. Check the documentation
2. Search existing issues
3. Create a new issue if needed

## Roadmap

- [ ] Implement online ticket purchasing
- [ ] Add performance reviews system
- [ ] Integrate with social media APIs
- [ ] Add multi-language support
- [ ] Implement real-time seat selection
- [ ] Add virtual tour feature

## Status

Project is: _in active development_

## Table of Contents

1. [Installation](#installation)
2. [Usage](#usage)
3. [Contributing](#contributing)
4. [Credits](#credits)
5. [License](#license)

## Usage

Instructions on how to use the project after installation.

## To-Do List

[ ] Fix the folder structure.
[ ] Update all dependencies
[ ] 
