using Microsoft.EntityFrameworkCore;
using Thu_y.Modules.AbttoirModule.Core;
using Thu_y.Modules.ReceiptModule.Core;
using Thu_y.Modules.ReportModule.Core;
using Thu_y.Modules.ShareModule.Core;
using Thu_y.Modules.UserModule.Core;

namespace Thu_y.Db.DbContext
{
    public sealed partial class AppDbContext
    {
        // User
        public DbSet<UserEntity> User { get; set; }
        public DbSet<UserScheduleEntity> UserSchedule { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }

        // Abattoir
        public DbSet<AbattoirEntity> Abattoir { get; set; }
        public DbSet<AbattoirDetailEntity> AbattoirDetail { get; set; }
        public DbSet<AnimalEntity> Animal { get; set; }
        public DbSet<VacineEntity> Vacine { get; set; }

        // Report entity
        public DbSet<ReportTicketEntity> ReportTicket { get; set; }
        public DbSet<ReportTicketValueEntity> ReportTicketValue { get; set; }
        public DbSet<FormEntity> Form { get; set; }
        public DbSet<FormAttributeEntity> FormAttribute { get; set; }
        public DbSet<SealConfigEntity> SealConfig { get; set; }
        public DbSet<SealTabEntity> SealTab { get; set; }
        public DbSet<ListAnimalEntity> ListAnimal { get; set; }

        // Receipt entity
        public DbSet<ReceiptAllocateEntity> ReceiptAllocate { get; set; }
        public DbSet<ReceiptEntity> Receipt { get; set; }
        public DbSet<ReceiptReportEntity> ReceiptReport { get; set; }
    }
}
