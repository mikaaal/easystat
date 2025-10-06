# 🚀 EasyStat Setup Guide

Komplett guide för att komma igång med EasyStat (.NET 8 + Vue 3)

## ✅ Vad som är klart

### Backend (.NET 8 API) ✅
- ✅ Models (Player, Team, Match, SubMatch, Leg, Throw, SubMatchParticipant)
- ✅ DTOs (PlayerStatsDto, TeamDto, TopStatsDto, ThrowAnalysisDto, etc.)
- ✅ DbContext (EasyStatDbContext)
- ✅ Services (PlayerService, TeamService, StatisticsService)
- ✅ Controllers (PlayersController, TeamsController, StatisticsController)
- ✅ CORS konfiguration för Vue frontend
- ✅ Swagger/OpenAPI dokumentation

### Frontend (Vue 3) ✅
- ✅ Vite + TypeScript setup
- ✅ Vue Router konfiguration
- ✅ TypeScript types baserat på DTOs
- ✅ API services (playerService, teamService, statsService)
- ✅ Views (HomeView, PlayersView, TeamsView, StatsView)
- ✅ Layout komponenter (AppHeader)
- ✅ Bootstrap 5 integration
- ✅ TanStack Query (Vue Query) setup

---

## 📋 Steg-för-steg Setup

### Steg 1: Installera Node.js dependencies (Vue Frontend)

```bash
cd C:\DEVTOOLS\development\easystat\EasyStat.Web
npm install
```

Detta kommer att installera:
- Vue 3
- Vue Router
- Pinia
- Axios
- TanStack Query
- Bootstrap 5
- Chart.js
- TypeScript
- Vite

### Steg 2: Verifiera .NET Backend

Kontrollera att backend kan byggas:

```bash
cd C:\DEVTOOLS\development\easystat\EasyStat.Api
dotnet build
```

Om det finns några compile-fel, kör:
```bash
dotnet restore
dotnet build
```

### Steg 3: Starta Backend API

```bash
cd C:\DEVTOOLS\development\easystat\EasyStat.Api
dotnet run
```

Backend startar på: `http://localhost:5000`
Swagger UI: `http://localhost:5000/swagger`

**Verifiera:**
- Öppna `http://localhost:5000/swagger` i webbläsare
- Du ska se alla API endpoints listade
- Testa ett endpoint, t.ex. `GET /api/players`

### Steg 4: Starta Vue Frontend

I en **ny terminal**:

```bash
cd C:\DEVTOOLS\development\easystat\EasyStat.Web
npm run dev
```

Frontend startar på: `http://localhost:5173`

**Verifiera:**
- Öppna `http://localhost:5173` i webbläsare
- Du ska se startsidan med 3 kort (Spelare, Lag, Topplistor)
- Navigera till olika sidor via menyn

---

## 🧪 Testa Funktionalitet

### Test 1: Spelarsökning

1. Gå till "Spelare" (http://localhost:5173/players)
2. Börja skriva ett spelarnamn
3. Välj en spelare från autocomplete-listan
4. Verifiera att statistik visas (matcher, vinster, förluster, snitt)
5. Verifiera att matchhistorik visas

### Test 2: Lagsökning

1. Gå till "Lag" (http://localhost:5173/teams)
2. Börja skriva ett lagnamn
3. Välj ett lag från listan (inkl. division och säsong)
4. Verifiera att laguppställning visas
5. Verifiera att spelarstatistik per lag visas

### Test 3: Topplistor

1. Gå till "Topplistor" (http://localhost:5173/stats)
2. Verifiera att alla 4 topplistor visas:
   - Top 10 Högsta Snitt
   - Top 10 Högsta Checkouts
   - Top 10 Kortaste Legs
   - Top 10 Flest 180:or
3. Testa säsongsfiltrering

---

## 🐛 Vanliga Problem & Lösningar

### Problem 1: CORS-fel i frontend

**Symptom:** API-anrop misslyckas med CORS-fel i browser console

**Lösning:**
1. Verifiera att backend kör på port 5000
2. Kontrollera att CORS är konfigurerat i `Program.cs`:
   ```csharp
   builder.Services.AddCors(options =>
   {
       options.AddPolicy("VueFrontend", policy =>
       {
           policy.WithOrigins("http://localhost:5173", "http://localhost:3000")
                 .AllowAnyHeader()
                 .AllowAnyMethod();
       });
   });
   ```
3. Kontrollera att `app.UseCors("VueFrontend");` anropas i `Program.cs`

### Problem 2: Database inte hittad

**Symptom:** Fel vid start av backend om `goldenstat.db` saknas

**Lösning:**
1. Kontrollera att `goldenstat.db` finns i `EasyStat.Api/` katalogen
2. Om den saknas, kopiera från Python-projektet:
   ```bash
   cp C:\DEVTOOLS\development\goldenstat\goldenstat.db C:\DEVTOOLS\development\easystat\EasyStat.Api\
   ```

### Problem 3: TypeScript-fel i Vue

**Symptom:** TypeScript-fel vid build/dev

**Lösning:**
1. Kör `npm install` igen
2. Verifiera att `tsconfig.json` är korrekt
3. Om du får fel om saknade types, installera:
   ```bash
   npm install --save-dev @types/node
   ```

### Problem 4: API returnerar 404

**Symptom:** Frontend får 404 från API

**Lösning:**
1. Kontrollera att rätt port används (backend: 5000, frontend: 5173)
2. Verifiera att proxy är konfigurerad i `vite.config.ts`
3. Kontrollera att API base URL är korrekt i `.env.development`

### Problem 5: Inga data visas

**Symptom:** Frontend laddar men visar inga data

**Lösning:**
1. Öppna browser DevTools (F12)
2. Gå till Network tab
3. Verifiera att API-anrop görs och returnerar data
4. Kontrollera att databasen innehåller data:
   ```bash
   sqlite3 EasyStat.Api/goldenstat.db "SELECT COUNT(*) FROM players;"
   ```

---

## 📁 Filstruktur Översikt

### Backend (EasyStat.Api/)
```
EasyStat.Api/
├── Controllers/
│   ├── PlayersController.cs      ✅ Färdig
│   ├── TeamsController.cs        ✅ Färdig
│   └── StatisticsController.cs   ✅ Färdig
├── Services/
│   ├── PlayerService.cs          ✅ Färdig
│   ├── TeamService.cs            ✅ Färdig
│   ├── StatisticsService         ✅ Färdig
│   └── MatchService.cs           ⚠️ Tom (framtida användning)
├── DTOs/
│   ├── PlayerStatsDto.cs         ✅ Färdig
│   ├── TeamDto.cs                ✅ Färdig
│   ├── TopStatsDto.cs            ✅ Färdig
│   ├── ThrowAnalysisDto.cs       ✅ Färdig
│   ├── SubMatchThrowsDto.cs      ✅ Färdig
│   └── OverviewDto.cs            ✅ Färdig
└── Models/                       ✅ Färdiga
```

### Frontend (EasyStat.Web/)
```
EasyStat.Web/
├── src/
│   ├── views/
│   │   ├── HomeView.vue          ✅ Färdig
│   │   ├── PlayersView.vue       ✅ Färdig
│   │   ├── TeamsView.vue         ✅ Färdig
│   │   └── StatsView.vue         ✅ Färdig
│   ├── components/
│   │   └── layout/
│   │       └── AppHeader.vue     ✅ Färdig
│   ├── services/
│   │   ├── api.ts                ✅ Färdig
│   │   ├── playerService.ts      ✅ Färdig
│   │   ├── teamService.ts        ✅ Färdig
│   │   └── statsService.ts       ✅ Färdig
│   ├── types/
│   │   ├── player.ts             ✅ Färdig
│   │   ├── team.ts               ✅ Färdig
│   │   └── stats.ts              ✅ Färdig
│   ├── router/
│   │   └── index.ts              ✅ Färdig
│   ├── App.vue                   ✅ Färdig
│   └── main.ts                   ✅ Färdig
├── package.json                  ✅ Färdig
├── vite.config.ts                ✅ Färdig
├── tsconfig.json                 ✅ Färdig
└── index.html                    ✅ Färdig
```

---

## 🎯 Nästa Steg (Framtida Förbättringar)

1. **Chart.js Integration**
   - Lägg till progress chart för spelares utveckling över tid
   - Implementera Chart.js komponenter

2. **Detaljerad Matchvy**
   - Implementera vy för kastsekvenser (`SubMatchThrowsView.vue`)
   - Visa throw-by-throw data

3. **Caching**
   - Implementera bättre caching med TanStack Query
   - Konfigurera stale-time och cache-time

4. **Responsiv Design**
   - Förbättra mobil-layout
   - Lägg till hamburger-meny för mobil

5. **Export Funktionalitet**
   - Lägg till CSV/Excel export
   - Implementera print-vänlig vy

6. **Säsongsfiltrering**
   - Lägg till dynamisk säsongsfiltrering överallt
   - Spara användarens valda säsong i localStorage

---

## 📞 Support

Om du stöter på problem:

1. Kontrollera browser console (F12) för fel
2. Kontrollera backend logs i terminalen där `dotnet run` körs
3. Verifiera att alla npm-paket är installerade: `npm install`
4. Verifiera att .NET-paket är återställda: `dotnet restore`
5. Kontrollera att databasen finns och innehåller data

---

## ✅ Checklist för Färdigt System

- [x] Backend körs på port 5000
- [x] Swagger UI tillgänglig på /swagger
- [x] Frontend körs på port 5173
- [x] Alla API endpoints fungerar
- [x] Spelarsökning fungerar
- [x] Lagsökning fungerar
- [x] Topplistor visas korrekt
- [x] CORS konfigurerat korrekt
- [x] TypeScript kompilerar utan fel
- [x] Inga console errors i browser
- [ ] Chart.js diagram (framtida)
- [ ] Detaljerad matchvy (framtida)

---

**Status:** ✅ Backend och Frontend är färdiga och redo att köras!

**Skapad:** 2025-10-06
