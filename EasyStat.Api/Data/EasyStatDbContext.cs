using EasyStat.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyStat.Api.Data;

public class EasyStatDbContext : DbContext
{
    public EasyStatDbContext(DbContextOptions<EasyStatDbContext> options) : base(options)
    {
    }

    public DbSet<Player> Players { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Match> Matches { get; set; }
    public DbSet<SubMatch> SubMatches { get; set; }
    public DbSet<SubMatchParticipant> SubMatchParticipants { get; set; }
    public DbSet<Leg> Legs { get; set; }
    public DbSet<Throw> Throws { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Player
        modelBuilder.Entity<Player>(entity =>
        {
            entity.ToTable("players");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(255).IsRequired();
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            
            entity.HasIndex(e => e.Name).HasDatabaseName("idx_players_name");
        });

        // Team
        modelBuilder.Entity<Team>(entity =>
        {
            entity.ToTable("teams");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(255).IsRequired();
            entity.Property(e => e.Division).HasColumnName("division").HasMaxLength(255);
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // Match
        modelBuilder.Entity<Match>(entity =>
        {
            entity.ToTable("matches");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MatchUrl).HasColumnName("match_url").HasMaxLength(500).IsRequired();
            entity.Property(e => e.Team1Id).HasColumnName("team1_id");
            entity.Property(e => e.Team2Id).HasColumnName("team2_id");
            entity.Property(e => e.Team1Score).HasColumnName("team1_score");
            entity.Property(e => e.Team2Score).HasColumnName("team2_score");
            entity.Property(e => e.Team1Avg).HasColumnName("team1_avg").HasColumnType("decimal(5,2)");
            entity.Property(e => e.Team2Avg).HasColumnName("team2_avg").HasColumnType("decimal(5,2)");
            entity.Property(e => e.Division).HasColumnName("division").HasMaxLength(255);
            entity.Property(e => e.Season).HasColumnName("season").HasMaxLength(255);
            entity.Property(e => e.MatchDate).HasColumnName("match_date");
            entity.Property(e => e.ScrapedAt).HasColumnName("scraped_at");

            entity.HasOne(e => e.Team1)
                .WithMany(t => t.HomeMatches)
                .HasForeignKey(e => e.Team1Id)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Team2)
                .WithMany(t => t.AwayMatches)
                .HasForeignKey(e => e.Team2Id)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasIndex(e => new { e.Team1Id, e.Team2Id }).HasDatabaseName("idx_matches_teams");
            entity.HasIndex(e => e.MatchDate).HasDatabaseName("idx_matches_date");
        });

        // SubMatch
        modelBuilder.Entity<SubMatch>(entity =>
        {
            entity.ToTable("sub_matches");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MatchId).HasColumnName("match_id");
            entity.Property(e => e.MatchNumber).HasColumnName("match_number");
            entity.Property(e => e.MatchType).HasColumnName("match_type").HasMaxLength(50).IsRequired();
            entity.Property(e => e.MatchName).HasColumnName("match_name").HasMaxLength(100);
            entity.Property(e => e.Team1Legs).HasColumnName("team1_legs");
            entity.Property(e => e.Team2Legs).HasColumnName("team2_legs");
            entity.Property(e => e.Team1Avg).HasColumnName("team1_avg").HasColumnType("decimal(5,2)");
            entity.Property(e => e.Team2Avg).HasColumnName("team2_avg").HasColumnType("decimal(5,2)");
            entity.Property(e => e.Mid).HasColumnName("mid").HasMaxLength(255);
            entity.Property(e => e.ScrapedAt).HasColumnName("scraped_at");

            entity.HasOne(e => e.Match)
                .WithMany(m => m.SubMatches)
                .HasForeignKey(e => e.MatchId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasIndex(e => e.MatchId).HasDatabaseName("idx_sub_matches_match");
        });

        // SubMatchParticipant
        modelBuilder.Entity<SubMatchParticipant>(entity =>
        {
            entity.ToTable("sub_match_participants");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SubMatchId).HasColumnName("sub_match_id");
            entity.Property(e => e.PlayerId).HasColumnName("player_id");
            entity.Property(e => e.TeamNumber).HasColumnName("team_number");
            entity.Property(e => e.PlayerAvg).HasColumnName("player_avg").HasColumnType("decimal(5,2)");

            entity.HasOne(e => e.SubMatch)
                .WithMany(sm => sm.Participants)
                .HasForeignKey(e => e.SubMatchId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Player)
                .WithMany(p => p.Participations)
                .HasForeignKey(e => e.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasIndex(e => e.PlayerId).HasDatabaseName("idx_participants_player");
            entity.HasIndex(e => e.SubMatchId).HasDatabaseName("idx_participants_sub_match");
        });

        // Leg
        modelBuilder.Entity<Leg>(entity =>
        {
            entity.ToTable("legs");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SubMatchId).HasColumnName("sub_match_id");
            entity.Property(e => e.LegNumber).HasColumnName("leg_number");
            entity.Property(e => e.WinnerTeam).HasColumnName("winner_team");
            entity.Property(e => e.FirstPlayerTeam).HasColumnName("first_player_team");
            entity.Property(e => e.TotalRounds).HasColumnName("total_rounds");
            entity.Property(e => e.ScrapedAt).HasColumnName("scraped_at");

            entity.HasOne(e => e.SubMatch)
                .WithMany(sm => sm.Legs)
                .HasForeignKey(e => e.SubMatchId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasIndex(e => e.SubMatchId).HasDatabaseName("idx_legs_sub_match");
        });

        // Throw
        modelBuilder.Entity<Throw>(entity =>
        {
            entity.ToTable("throws");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LegId).HasColumnName("leg_id");
            entity.Property(e => e.TeamNumber).HasColumnName("team_number");
            entity.Property(e => e.RoundNumber).HasColumnName("round_number");
            entity.Property(e => e.Score).HasColumnName("score");
            entity.Property(e => e.RemainingScore).HasColumnName("remaining_score");
            entity.Property(e => e.DartsUsed).HasColumnName("darts_used");

            entity.HasOne(e => e.Leg)
                .WithMany(l => l.Throws)
                .HasForeignKey(e => e.LegId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasIndex(e => e.LegId).HasDatabaseName("idx_throws_leg");
        });
    }
}