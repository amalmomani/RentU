using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace RentU.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Raboutu> Raboutus { get; set; }
        public virtual DbSet<Rbank> Rbanks { get; set; }
        public virtual DbSet<Rcategory> Rcategories { get; set; }
        public virtual DbSet<Rcontactu> Rcontactus { get; set; }
        public virtual DbSet<Rmainpage> Rmainpages { get; set; }
        public virtual DbSet<Rorder> Rorders { get; set; }
        public virtual DbSet<Rorderproduct> Rorderproducts { get; set; }
        public virtual DbSet<Rpayment> Rpayments { get; set; }
        public virtual DbSet<Rproduct> Rproducts { get; set; }
        public virtual DbSet<Rrole> Rroles { get; set; }
        public virtual DbSet<Rtestimonial> Rtestimonials { get; set; }
        public virtual DbSet<Ruseraccount> Ruseraccounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("USER ID=TAH13_User46;PASSWORD=amal;DATA SOURCE=94.56.229.181:3488/traindb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TAH13_USER46")
                .HasAnnotation("Relational:Collation", "USING_NLS_COMP");

            modelBuilder.Entity<Raboutu>(entity =>
            {
                entity.HasKey(e => e.Aboutid)
                    .HasName("SYS_C00242950");

                entity.ToTable("RABOUTUS");

                entity.Property(e => e.Aboutid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ABOUTID");

                entity.Property(e => e.Image)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE");

                entity.Property(e => e.Text1)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("TEXT1");

                entity.Property(e => e.Text2)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("TEXT2");

                entity.Property(e => e.Text3)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("TEXT3");

                entity.Property(e => e.Text4)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("TEXT4");
            });

            modelBuilder.Entity<Rbank>(entity =>
            {
                entity.HasKey(e => e.Payid)
                    .HasName("SYS_C00242944");

                entity.ToTable("RBANK");

                entity.Property(e => e.Payid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PAYID");

                entity.Property(e => e.Amount)
                    .HasColumnType("NUMBER")
                    .HasColumnName("AMOUNT");

                entity.Property(e => e.Cardnumber)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CARDNUMBER");

                entity.Property(e => e.Cvv)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CVV");

                entity.Property(e => e.Expiration)
                    .HasColumnType("DATE")
                    .HasColumnName("EXPIRATION");

                entity.Property(e => e.Ownername)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("OWNERNAME");
            });

            modelBuilder.Entity<Rcategory>(entity =>
            {
                entity.HasKey(e => e.Categoryid)
                    .HasName("SYS_C00242938");

                entity.ToTable("RCATEGORY");

                entity.Property(e => e.Categoryid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CATEGORYID");

                entity.Property(e => e.Categoryname)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("CATEGORYNAME");

                entity.Property(e => e.Image)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE");
            });

            modelBuilder.Entity<Rcontactu>(entity =>
            {
                entity.HasKey(e => e.Contid)
                    .HasName("SYS_C00242946");

                entity.ToTable("RCONTACTUS");

                entity.Property(e => e.Contid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CONTID");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Message)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("MESSAGE");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<Rmainpage>(entity =>
            {
                entity.HasKey(e => e.Homeid)
                    .HasName("SYS_C00242948");

                entity.ToTable("RMAINPAGE");

                entity.Property(e => e.Homeid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("HOMEID");

                entity.Property(e => e.Companyemail)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("COMPANYEMAIL");

                entity.Property(e => e.Companylogo)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("COMPANYLOGO");

                entity.Property(e => e.Companyphone)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("COMPANYPHONE");

                entity.Property(e => e.Image1)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE1");

                entity.Property(e => e.Image2)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE2");

                entity.Property(e => e.Text1)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("TEXT1");

                entity.Property(e => e.Text2)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("TEXT2");
            });

            modelBuilder.Entity<Rorder>(entity =>
            {
                entity.HasKey(e => e.Orderid)
                    .HasName("SYS_C00242959");

                entity.ToTable("RORDER");

                entity.Property(e => e.Orderid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ORDERID");

                entity.Property(e => e.Orderdate)
                    .HasColumnType("DATE")
                    .HasColumnName("ORDERDATE");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Rorders)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FKUSERSS");
            });

            modelBuilder.Entity<Rorderproduct>(entity =>
            {
                entity.ToTable("RORDERPRODUCT");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Numberofpieces)
                    .HasColumnType("NUMBER")
                    .HasColumnName("NUMBEROFPIECES");

                entity.Property(e => e.Orderid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ORDERID");

                entity.Property(e => e.Productid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PRODUCTID");

                entity.Property(e => e.Status)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.Property(e => e.Totalamount)
                    .HasColumnType("FLOAT")
                    .HasColumnName("TOTALAMOUNT");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Rorderproducts)
                    .HasForeignKey(d => d.Orderid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("PRODUCTF");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Rorderproducts)
                    .HasForeignKey(d => d.Productid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FKPRODUCTT");
            });

            modelBuilder.Entity<Rpayment>(entity =>
            {
                entity.HasKey(e => e.Payid)
                    .HasName("SYS_C00242970");

                entity.ToTable("RPAYMENT");

                entity.Property(e => e.Payid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PAYID");

                entity.Property(e => e.Amount)
                    .HasColumnType("FLOAT")
                    .HasColumnName("AMOUNT");

                entity.Property(e => e.Cardnumber)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CARDNUMBER");

                entity.Property(e => e.Paydate)
                    .HasColumnType("DATE")
                    .HasColumnName("PAYDATE");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Rpayments)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("USERFK");
            });

            modelBuilder.Entity<Rproduct>(entity =>
            {
                entity.HasKey(e => e.Productid)
                    .HasName("SYS_C00243081");

                entity.ToTable("RPRODUCT");

                entity.Property(e => e.Productid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PRODUCTID");

                entity.Property(e => e.Categoryid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CATEGORYID");

                entity.Property(e => e.Costtopost)
                    .HasColumnType("NUMBER")
                    .HasColumnName("COSTTOPOST");

                entity.Property(e => e.Image)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE");

                entity.Property(e => e.Price)
                    .HasColumnType("FLOAT")
                    .HasColumnName("PRICE");

                entity.Property(e => e.Productname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PRODUCTNAME");

                entity.Property(e => e.Proof)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("PROOF");

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Rproducts)
                    .HasForeignKey(d => d.Categoryid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("PRODF");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Rproducts)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("USERIDD");
            });

            modelBuilder.Entity<Rrole>(entity =>
            {
                entity.HasKey(e => e.Roleid)
                    .HasName("SYS_C00242936");

                entity.ToTable("RROLES");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ROLEID");

                entity.Property(e => e.Rolename)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ROLENAME");
            });

            modelBuilder.Entity<Rtestimonial>(entity =>
            {
                entity.HasKey(e => e.Testmoninalid)
                    .HasName("SYS_C00243004");

                entity.ToTable("RTESTIMONIAL");

                entity.Property(e => e.Testmoninalid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("TESTMONINALID");

                entity.Property(e => e.Message)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("MESSAGE");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("STATUS")
                    .IsFixedLength(true);

                entity.Property(e => e.Testimage)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TESTIMAGE");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Rtestimonials)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("TEST");
            });

            modelBuilder.Entity<Ruseraccount>(entity =>
            {
                entity.HasKey(e => e.Userid)
                    .HasName("SYS_C00242954");

                entity.ToTable("RUSERACCOUNT");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("USERID");

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("FULLNAME");

                entity.Property(e => e.Image)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.Phonenumber)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PHONENUMBER");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ROLEID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Ruseraccounts)
                    .HasForeignKey(d => d.Roleid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("ROLEF");
            });

            modelBuilder.HasSequence("DEP").IncrementsBy(5);

            modelBuilder.HasSequence("DEPART");

            modelBuilder.HasSequence("ODD_EVEN");

            modelBuilder.HasSequence("SUPLIER_SEQ");

            modelBuilder.HasSequence("SUPLIER_SEQ1").IncrementsBy(10);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
