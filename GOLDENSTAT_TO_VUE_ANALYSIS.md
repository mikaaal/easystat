# GoldenStat - Analys och Vue Migration Plan

## 📊 Befintlig App-struktur (Flask)

### Teknisk Stack
- **Backend:** Flask (Python)
- **Frontend:** Vanilla JavaScript med Bootstrap 5
- **Database:** SQLite (goldenstat.db)
- **Charts:** Chart.js
- **Icons:** Bootstrap Icons

### Projektstruktur
```
goldenstat/
├── app.py                    # Flask backend med alla API endpoints
├── database.py               # SQLite database wrapper
├── templates/
│   ├── base.html            # Base template med navigation
│   ├── index.html           # Huvudsida med 3 tabs
│   └── sub_match_throws.html # Detaljerad match-vy
├── static/
│   ├── js/
│   │   └── app.js           # Vanilla JS applikationslogik
│   ├── css/
│   │   └── style.css        # Custom CSS styling
│   └── images/
├── goldenstat.db            # SQLite databas
└── requirements.txt
```

---

## 🎯 Huvudfunktionalitet

### 1. **Spelarstatistik** (Players Tab)
- **Sökfunktion:** Autocomplete för spelarsökning
- **Filtreringsmöjligheter:** Efter säsong
- **Statistikvisning:**
  - Översiktskort (totalt matcher, vinster, förluster, snitt)
  - Singlar-statistik (separate från dubblar)
  - Dubblar-statistik
  - Progress chart (Chart.js line chart)
  - Match-historik tabell
  - Detaljerad kastvyer per match

### 2. **Lagstatistik** (Teams Tab)
- **Lagsökning:** Autocomplete med division och säsong
- **Laguppställning:**
  - Lista på alla spelare i laget
  - Individuell statistik per spelare
  - Totala snitt för laget

### 3. **Topplistor** (Stats Tab)
- Top 10 högsta snitt (singlar)
- Top 10 högsta checkouts
- Top 10 kortaste legs (minst darts)
- Top 10 flest 180:or i en match
- **Filtrerbar efter säsong**

### 4. **Detaljerade Matchvyer**
- Separat sida för kastsekvenser (`sub_match_throws.html`)
- Visar alla kast i en match
- Leg-by-leg breakdown
- Visualisering av kast per rond

---

## 🔌 API Endpoints (Flask Backend)

### Public/Info Endpoints
```
GET  /                                          # Main page
GET  /api/last-import                          # Last import info
GET  /api/overview                             # Database overview stats
```

### Player Endpoints
```
GET  /api/players                              # All players (autocomplete)
GET  /api/player/<player_name>                 # Player basic stats
GET  /api/player/<player_name>/detailed        # Player detailed stats
GET  /api/player/<player_name>/throws          # Player throw history
GET  /api/player/<int:player_id>/memorable-matches  # Top matches
```

### Team Endpoints
```
GET  /api/teams                                # All teams (autocomplete)
GET  /api/team/<team_name>/lineup              # Team lineup & stats
GET  /api/team/<team_name>/players             # Team player list
```

### Match/Sub-Match Endpoints
```
GET  /api/sub_match/<int:sub_match_id>         # Sub-match details
GET  /api/sub_match/<int:sub_match_id>/throws/<player_name>  # Throws for player in sub-match
GET  /api/match/<int:match_id>/legs            # All legs in match
```

### Stats/Analytics Endpoints
```
GET  /api/top-stats?season=<season>            # Top 10 lists (filterable)
GET  /api/leagues                              # All leagues/divisions
```

### Legacy/Test Endpoints
```
GET  /player/<player_name>                     # Legacy redirect
GET  /sub_match_throws                         # Detailed match view page
GET  /test_throws                              # Test endpoint
```

---

## 📦 Data Models (från database.py och app.py)

### Player
```typescript
{
  id: number
  name: string
  created_at: datetime
}
```

### Team
```typescript
{
  id: number
  name: string
  division: string
  created_at: datetime
}
```

### Match
```typescript
{
  id: number
  match_url: string
  team1_id: number
  team2_id: number
  team1_score: number
  team2_score: number
  team1_avg: number
  team2_avg: number
  division: string
  season: string
  match_date: datetime
  scraped_at: datetime
}
```

### SubMatch
```typescript
{
  id: number
  match_id: number
  match_number: number
  match_type: 'Singles' | 'Doubles'
  match_name: string
  team1_legs: number
  team2_legs: number
  team1_avg: number
  team2_avg: number
  mid: string
  scraped_at: datetime
}
```

### SubMatchParticipant
```typescript
{
  id: number
  sub_match_id: number
  player_id: number
  team_number: 1 | 2
  player_avg: number
}
```

### Leg
```typescript
{
  id: number
  sub_match_id: number
  leg_number: number
  winner_team: 1 | 2
  first_player_team: 1 | 2
  total_rounds: number
}
```

### Throw
```typescript
{
  id: number
  leg_id: number
  team_number: 1 | 2
  round_number: number
  score: number
  remaining_score: number
  darts_used: number
}
```

---

## 🎨 UI/UX Komponenter (från befintlig app)

### Layoutkomponenter
- **Sticky Tabs Navigation** - 3 tabs (Spelare, Lag, Topplistor)
- **Search Bar** - Med autocomplete dropdown
- **Result Cards** - Bootstrap cards för resultatvisning
- **Filter Section** - Säsongsfiltrering

### Datavisualiseringskomponenter
- **Stats Overview Cards** - 4 statistik-kort (matcher, vinster, förluster, snitt)
- **Progress Chart** - Line chart med glidande medelvärde
- **Match History Table** - Sortbar tabell med matcher
- **Top 10 Lists** - Tabeller för olika topplistor
- **Throw Sequence Viewer** - Detaljerad kastsekvens per leg

### Interaktionskomponenter
- **Autocomplete Search** - För spelare och lag
- **Season Filter Dropdown**
- **Tab Navigation**
- **Notification System** - Toast-meddelanden
- **Collapse/Expand** - För matchdetaljer

---

## 🚀 Vue Migration Plan

### Rekommenderad Tech Stack

#### Core
- **Vue 3** (Composition API)
- **TypeScript**
- **Vite** (build tool)
- **Vue Router** (client-side routing)
- **Pinia** (state management)

#### UI/Styling
- **Bootstrap 5** (eller **PrimeVue** för mer Vue-native komponenter)
- **Bootstrap Icons** (eller ersätt med **Heroicons**)
- **Chart.js** + **vue-chartjs** (för diagram)

#### API/Data
- **Axios** (HTTP client)
- **TanStack Query (Vue Query)** (data fetching & caching)

### Projektstruktur (Vue 3)

```
easystat-vue/
├── public/
│   └── favicon.ico
├── src/
│   ├── main.ts                    # Vue app entry point
│   ├── App.vue                    # Root component
│   ├── router/
│   │   └── index.ts              # Vue Router config
│   ├── stores/                   # Pinia stores
│   │   ├── player.ts
│   │   ├── team.ts
│   │   └── stats.ts
│   ├── views/                    # Page-level components
│   │   ├── HomeView.vue
│   │   ├── PlayerView.vue
│   │   ├── TeamView.vue
│   │   ├── StatsView.vue
│   │   └── SubMatchThrowsView.vue
│   ├── components/               # Reusable components
│   │   ├── layout/
│   │   │   ├── AppHeader.vue
│   │   │   ├── AppTabs.vue
│   │   │   └── AppFooter.vue
│   │   ├── search/
│   │   │   ├── PlayerSearch.vue
│   │   │   ├── TeamSearch.vue
│   │   │   └── Autocomplete.vue
│   │   ├── stats/
│   │   │   ├── StatsOverview.vue
│   │   │   ├── ProgressChart.vue
│   │   │   ├── MatchHistory.vue
│   │   │   └── TopLists.vue
│   │   ├── player/
│   │   │   ├── PlayerCard.vue
│   │   │   ├── PlayerStats.vue
│   │   │   └── PlayerFilters.vue
│   │   ├── team/
│   │   │   ├── TeamLineup.vue
│   │   │   └── TeamPlayerList.vue
│   │   ├── match/
│   │   │   ├── MatchCard.vue
│   │   │   ├── ThrowSequence.vue
│   │   │   └── LegBreakdown.vue
│   │   └── common/
│   │       ├── LoadingSpinner.vue
│   │       ├── ErrorMessage.vue
│   │       ├── NotificationToast.vue
│   │       └── SeasonFilter.vue
│   ├── composables/              # Vue composables
│   │   ├── useApi.ts
│   │   ├── usePlayer.ts
│   │   ├── useTeam.ts
│   │   ├── useStats.ts
│   │   └── useNotification.ts
│   ├── services/                 # API services
│   │   ├── api.ts               # Axios instance
│   │   ├── playerService.ts
│   │   ├── teamService.ts
│   │   └── statsService.ts
│   ├── types/                    # TypeScript types
│   │   ├── player.ts
│   │   ├── team.ts
│   │   ├── match.ts
│   │   └── stats.ts
│   ├── utils/                    # Utility functions
│   │   ├── formatters.ts        # Date, number formatting
│   │   └── charts.ts            # Chart.js helpers
│   └── assets/
│       ├── styles/
│       │   ├── main.css
│       │   └── variables.css
│       └── images/
├── .env.development
├── .env.production
├── vite.config.ts
├── tsconfig.json
├── package.json
└── README.md
```

---

## 📋 Implementation Roadmap

### Phase 1: Setup & Foundation
1. ✅ Initialize Vue 3 + Vite + TypeScript project
2. ✅ Setup Vue Router
3. ✅ Setup Pinia store
4. ✅ Install Bootstrap 5 / PrimeVue
5. ✅ Install Chart.js + vue-chartjs
6. ✅ Setup Axios + API base configuration
7. ✅ Create TypeScript types/interfaces

### Phase 2: Core Components
1. Create layout components (Header, Tabs, Footer)
2. Create common components (Loading, Error, Notification)
3. Create Autocomplete search component
4. Create Season filter component

### Phase 3: Player Features
1. Create PlayerSearch view/component
2. Implement player autocomplete
3. Create PlayerStats component
4. Create ProgressChart component
5. Create MatchHistory component
6. Integrate player API endpoints

### Phase 4: Team Features
1. Create TeamSearch view/component
2. Implement team autocomplete
3. Create TeamLineup component
4. Create TeamPlayerList component
5. Integrate team API endpoints

### Phase 5: Stats Features
1. Create StatsView with top lists
2. Create TopLists component (reusable)
3. Add season filtering
4. Integrate stats API endpoints

### Phase 6: Match Details
1. Create SubMatchThrowsView
2. Create ThrowSequence component
3. Create LegBreakdown component
4. Add match detail routing

### Phase 7: Polish & Optimization
1. Add loading states
2. Add error handling
3. Implement caching (Vue Query)
4. Add responsive design improvements
5. Performance optimization
6. SEO optimization (if needed)

### Phase 8: Deployment
1. Environment configuration
2. Build optimization
3. Docker setup (optional)
4. Deploy to production

---

## 🔄 API Backend Strategy

### Option 1: Keep Flask Backend (Recommended for MVP)
**Pros:**
- No backend rewrite needed
- Existing API endpoints work as-is
- Quick migration focus on frontend only

**Cons:**
- Python/Flask maintenance
- CORS configuration needed

### Option 2: Migrate to C# ASP.NET Core
**Pros:**
- Matches existing EasyStat.Api project
- Better integration with Entity Framework
- Type-safe with C# models

**Cons:**
- Requires rewriting all API endpoints
- More work upfront

### Recommendation:
**Start with Option 1** - Keep Flask backend and focus on Vue frontend migration.
Later, if needed, migrate backend to C# ASP.NET Core incrementally.

---

## 🎯 Key Differences: Flask App vs Vue App

### Routing
| Flask (Server-side) | Vue (Client-side) |
|---------------------|-------------------|
| `@app.route('/')` serves HTML | `router.push('/')` changes view |
| Each route renders a template | SPA - single HTML, dynamic views |
| Page reloads on navigation | No page reloads |

### State Management
| Flask | Vue |
|-------|-----|
| Session-based or stateless | Pinia stores (reactive) |
| Data fetched per request | Cached & shared across components |

### Data Fetching
| Flask | Vue |
|-------|-----|
| Jinja template receives data | Axios/Fetch API calls |
| Server renders HTML with data | Client renders with JSON data |

---

## 🛠️ Recommended Libraries

```json
{
  "dependencies": {
    "vue": "^3.4.0",
    "vue-router": "^4.2.0",
    "pinia": "^2.1.0",
    "axios": "^1.6.0",
    "@tanstack/vue-query": "^5.0.0",
    "bootstrap": "^5.3.0",
    "chart.js": "^4.4.0",
    "vue-chartjs": "^5.3.0",
    "bootstrap-icons": "^1.11.0"
  },
  "devDependencies": {
    "@vitejs/plugin-vue": "^5.0.0",
    "typescript": "^5.3.0",
    "vite": "^5.0.0",
    "vue-tsc": "^1.8.0"
  }
}
```

---

## 📝 Next Steps

1. **Review this document** - Confirm architecture decisions
2. **Initialize Vue project** - Run `npm create vue@latest`
3. **Setup development environment** - Install dependencies
4. **Start with Phase 1** - Foundation setup
5. **Iterate through phases** - Build incrementally

---

## 🎨 Design Considerations

### Keep from Original:
- Color scheme (blues, greens for charts)
- Bootstrap 5 components
- Chart.js visualizations
- Swedish language UI

### Improve:
- Better loading states
- Smoother transitions
- More responsive design
- Better error messages
- Optimistic UI updates

---

## 📊 Migration Checklist

- [ ] Initialize Vue 3 project
- [ ] Setup TypeScript
- [ ] Configure Vue Router
- [ ] Setup Pinia
- [ ] Install UI framework (Bootstrap/PrimeVue)
- [ ] Configure Axios
- [ ] Create type definitions
- [ ] Build layout components
- [ ] Implement Player search
- [ ] Implement Team search
- [ ] Implement Stats/Top lists
- [ ] Implement Match details view
- [ ] Add error handling
- [ ] Add loading states
- [ ] Test all features
- [ ] Performance optimization
- [ ] Deploy

---

*Generated: 2025-10-06*
*Source: C:\DEVTOOLS\development\goldenstat*
*Target: C:\DEVTOOLS\development\easystat (Vue 3 migration)*
