# EasyStat - Dart Statistics Platform

Modern dart statistics platform built with .NET 8 and Vue 3. Converted from Python/Flask to learn .NET and Vue.

## 🎯 Tech Stack

### Backend
- **.NET 8** - Web API
- **Entity Framework Core** - ORM with SQLite
- **AutoMapper** - Object mapping
- **Swagger** - API documentation

### Frontend
- **Vue 3** - Frontend framework
- **Composition API** - Modern Vue patterns
- **Vue Router** - Client-side routing
- **Pinia** - State management
- **Chart.js** - Data visualization
- **Bootstrap 5** - UI framework

### Data Collection
- **Python** - Web scraping scripts (from original project)
- **Playwright** - Browser automation
- **BeautifulSoup4** - HTML parsing

## 📁 Project Structure

```
easystat/
├── EasyStat.Api/              # .NET Web API
│   ├── Controllers/           # API endpoints
│   ├── Services/              # Business logic
│   ├── Models/                # Database entities
│   ├── DTOs/                  # Data transfer objects
│   ├── Data/                  # DbContext
│   └── goldenstat.db         # SQLite database
├── easystat-vue/              # Vue 3 Frontend
│   ├── src/
│   │   ├── components/       # Vue components
│   │   ├── views/            # Page views
│   │   ├── services/         # API services
│   │   ├── stores/           # Pinia stores
│   │   └── router/           # Vue Router
│   └── public/
└── scrapers/                  # Python scraping scripts
    ├── scraper.py
    ├── new_format_importer.py
    └── requirements.txt
```

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK
- Node.js 18+
- Python 3.11+ (for scrapers)

### Backend Setup

```bash
cd EasyStat.Api

# Restore packages
dotnet restore

# Run the API
dotnet run

# API will be available at http://localhost:5000
# Swagger UI at http://localhost:5000/swagger
```

### Frontend Setup

```bash
cd easystat-vue

# Install dependencies
npm install

# Run dev server
npm run dev

# Frontend will be available at http://localhost:5173
```

### Scraper Setup (Optional - for new data)

```bash
cd scrapers

# Create virtual environment
python3 -m venv venv
source venv/bin/activate

# Install dependencies
pip install -r requirements.txt
playwright install chromium

# Run scrapers
python new_format_importer.py <match_url>
```

## 📊 API Endpoints

### Players
- `GET /api/players` - Get all player names
- `GET /api/players/{name}` - Get player statistics
- `GET /api/players/{name}/throws` - Get throw analysis

### Statistics
- `GET /api/overview` - Get database overview
- `GET /api/sub_match/{id}/throws/{playerName}` - Get detailed match throws

## 🗄️ Database

The project uses the existing `goldenstat.db` SQLite database from the original Python project. Entity Framework Core maps to the existing schema without requiring migrations.

### Database Schema
- **players** - Player information
- **teams** - Team data
- **matches** - Complete team matches
- **sub_matches** - Individual games (Singles/Doubles)
- **sub_match_participants** - Player-game relationships
- **legs** - Individual legs
- **throws** - Throw-by-throw data

## 🎓 Learning Goals

This project serves as a learning platform for:
- ✅ .NET 8 Web API development
- ✅ Entity Framework Core
- ✅ Vue 3 with Composition API
- ✅ Modern frontend state management (Pinia)
- ✅ RESTful API design
- ✅ Single Page Application architecture

## 📝 Development Notes

- Backend uses existing database (no EF migrations needed)
- Python scrapers continue to populate data
- .NET API provides read-only access initially
- Vue frontend consumes REST API

## 🔧 Future Enhancements

- [ ] Real-time match updates
- [ ] Advanced analytics dashboard
- [ ] Player comparison tools
- [ ] Team statistics
- [ ] Export functionality
- [ ] User authentication

## 📄 License

MIT License - Learning project

## 🙏 Acknowledgments

Original Python/Flask project: GoldenStat
Data source: n01darts.com