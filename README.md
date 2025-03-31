# FirstLedger
A simple ledger API built using .NET 8.0 with features like:
- Money movements (deposits and withdrawals)
- Current balance viewing
- Transaction history

## Getting Started

### Prerequisites
- .NET 8.0 SDK
- Visual Studio or VS Code

### Installation
```bash
git clone https://github.com/j-hashemi/FirstLedger.git
cd FirstLedger
cd FirstLedger.Web
dotnet run
```
To view the swagger after running the application please add **/swagger/index.html** at the end of the given URL.

### Project Structure
- **Web**: Contains controllers to handle incoming HTTP requests and return responses.
- **Service**: Acts as the application logic layer, where business logic and processing occur.
- **Domain**: Contains core entities and domain-specific logic.
- **Repository**: Manages data persistence, responsible for storing and fetching data from the database.
- **Test**: Contains automated tests to validate the implemented logic and ensure functionality.


