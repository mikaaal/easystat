"""
Database wrapper for GoldenStat SQLite database
"""
import sqlite3
from datetime import datetime
from pathlib import Path
from typing import Dict, Optional, Tuple

# Get the absolute path to the database
SCRIPT_DIR = Path(__file__).parent.resolve()
DEFAULT_DB_PATH = SCRIPT_DIR.parent / "goldenstat.db"


class DartDatabase:
    """SQLite database wrapper for dart statistics"""

    def __init__(self, db_path: str = None):
        if db_path is None:
            db_path = str(DEFAULT_DB_PATH)
        self.db_path = db_path
        self._ensure_connection()

    def _ensure_connection(self):
        """Ensure database file exists"""
        if not Path(self.db_path).exists():
            raise FileNotFoundError(f"Database not found: {self.db_path}")

    def get_or_create_player(self, name: str) -> int:
        """Get existing player or create new one"""
        with sqlite3.connect(self.db_path) as conn:
            cursor = conn.cursor()

            # Try to find existing player
            cursor.execute("SELECT id FROM players WHERE name = ?", (name,))
            result = cursor.fetchone()

            if result:
                return result[0]

            # Create new player
            cursor.execute(
                "INSERT INTO players (name, created_at) VALUES (?, ?)",
                (name, datetime.now())
            )
            conn.commit()
            return cursor.lastrowid

    def get_or_create_team(self, name: str, division: str = None) -> int:
        """Get existing team or create new one"""
        with sqlite3.connect(self.db_path) as conn:
            cursor = conn.cursor()

            # Try to find existing team
            cursor.execute("SELECT id FROM teams WHERE name = ?", (name,))
            result = cursor.fetchone()

            if result:
                return result[0]

            # Create new team
            cursor.execute(
                "INSERT INTO teams (name, division, created_at) VALUES (?, ?, ?)",
                (name, division, datetime.now())
            )
            conn.commit()
            return cursor.lastrowid

    def insert_match(self, match_data: Dict) -> Tuple[int, bool]:
        """Insert match, return (match_id, is_new)"""
        with sqlite3.connect(self.db_path) as conn:
            cursor = conn.cursor()

            # Check if match already exists
            cursor.execute(
                "SELECT id FROM matches WHERE match_url = ?",
                (match_data['match_url'],)
            )
            result = cursor.fetchone()

            if result:
                return (result[0], False)

            # Insert new match
            cursor.execute("""
                INSERT INTO matches (
                    match_url, team1_id, team2_id, team1_score, team2_score,
                    team1_avg, team2_avg, division, season, match_date, scraped_at
                ) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)
            """, (
                match_data['match_url'],
                match_data['team1_id'],
                match_data['team2_id'],
                match_data['team1_score'],
                match_data['team2_score'],
                match_data['team1_avg'],
                match_data['team2_avg'],
                match_data['division'],
                match_data['season'],
                match_data['match_date'],
                datetime.now()
            ))
            conn.commit()
            return (cursor.lastrowid, True)

    def insert_sub_match(self, sub_match_data: Dict) -> int:
        """Insert sub-match, return sub_match_id"""
        with sqlite3.connect(self.db_path) as conn:
            cursor = conn.cursor()

            cursor.execute("""
                INSERT INTO sub_matches (
                    match_id, match_number, match_type, match_name,
                    team1_legs, team2_legs, team1_avg, team2_avg, mid, scraped_at
                ) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)
            """, (
                sub_match_data['match_id'],
                sub_match_data['match_number'],
                sub_match_data['match_type'],
                sub_match_data['match_name'],
                sub_match_data['team1_legs'],
                sub_match_data['team2_legs'],
                sub_match_data.get('team1_avg', 0),
                sub_match_data.get('team2_avg', 0),
                sub_match_data.get('mid', ''),
                datetime.now()
            ))
            conn.commit()
            return cursor.lastrowid

    def insert_sub_match_participant(self, participant_data: Dict):
        """Insert sub-match participant"""
        with sqlite3.connect(self.db_path) as conn:
            cursor = conn.cursor()

            cursor.execute("""
                INSERT INTO sub_match_participants (
                    sub_match_id, player_id, team_number, player_avg
                ) VALUES (?, ?, ?, ?)
            """, (
                participant_data['sub_match_id'],
                participant_data['player_id'],
                participant_data['team_number'],
                participant_data['player_avg']
            ))
            conn.commit()

    def insert_leg(self, leg_data: Dict) -> int:
        """Insert leg, return leg_id"""
        with sqlite3.connect(self.db_path) as conn:
            cursor = conn.cursor()

            cursor.execute("""
                INSERT INTO legs (
                    sub_match_id, leg_number, winner_team, first_player_team, total_rounds
                ) VALUES (?, ?, ?, ?, ?)
            """, (
                leg_data['sub_match_id'],
                leg_data['leg_number'],
                leg_data['winner_team'],
                leg_data['first_player_team'],
                leg_data['total_rounds']
            ))
            conn.commit()
            return cursor.lastrowid

    def insert_throw(self, throw_data: Dict):
        """Insert throw"""
        with sqlite3.connect(self.db_path) as conn:
            cursor = conn.cursor()

            cursor.execute("""
                INSERT INTO throws (
                    leg_id, team_number, round_number, score, remaining_score, darts_used
                ) VALUES (?, ?, ?, ?, ?, ?)
            """, (
                throw_data['leg_id'],
                throw_data['team_number'],
                throw_data['round_number'],
                throw_data['score'],
                throw_data['remaining_score'],
                throw_data['darts_used']
            ))
            conn.commit()
