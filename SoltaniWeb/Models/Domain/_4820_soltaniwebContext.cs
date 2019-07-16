using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SoltaniWeb.Models.Domain
{
    public partial class _4820_soltaniwebContext : DbContext
    {
        public _4820_soltaniwebContext()
        {
        }

        public _4820_soltaniwebContext(DbContextOptions<_4820_soltaniwebContext> options)
            : base(options)
        {
        }

        public virtual DbSet<tbl_accesslevels> tbl_accesslevels { get; set; }
        public virtual DbSet<tbl_accesstypes> tbl_accesstypes { get; set; }
        public virtual DbSet<tbl_actionaccessibility> tbl_actionaccessibility { get; set; }
        public virtual DbSet<tbl_actionaccesstype> tbl_actionaccesstype { get; set; }
        public virtual DbSet<tbl_actions> tbl_actions { get; set; }
        public virtual DbSet<tbl_anbar> tbl_anbar { get; set; }
        public virtual DbSet<tbl_appcontactsinfo> tbl_appcontactsinfo { get; set; }
        public virtual DbSet<tbl_applicantrelativeinfo> tbl_applicantrelativeinfo { get; set; }
        public virtual DbSet<tbl_applicants> tbl_applicants { get; set; }
        public virtual DbSet<tbl_branches> tbl_branches { get; set; }
        public virtual DbSet<tbl_category> tbl_category { get; set; }
        public virtual DbSet<tbl_contactus> tbl_contactus { get; set; }
        public virtual DbSet<tbl_contactype> tbl_contactype { get; set; }
        public virtual DbSet<tbl_Company> tbl_Company { get; set; }
        public virtual DbSet<tbl_CompanyPerson> tbl_CompanyPerson { get; set; }
        public virtual DbSet<tbl_CompanySection> tbl_CompanySection { get; set; }
        public virtual DbSet<tbl_controllers> tbl_controllers { get; set; }
        public virtual DbSet<tbl_DecorType> tbl_DecorType { get; set; }
        public virtual DbSet<tbl_discount> tbl_discount { get; set; }
        public virtual DbSet<tbl_educationaldegree> tbl_educationaldegree { get; set; }
        public virtual DbSet<tbl_educationalrecords> tbl_educationalrecords { get; set; }
        public virtual DbSet<tbl_files> tbl_files { get; set; }
        public virtual DbSet<tbl_filestosend> tbl_filestosend { get; set; }
        public virtual DbSet<tbl_ftp> tbl_ftp { get; set; }
        public virtual DbSet<tbl_havaletype> tbl_havaletype { get; set; }
        public virtual DbSet<tbl_historyappconnect> tbl_historyappconnect { get; set; }
        public virtual DbSet<tbl_job> tbl_job { get; set; }
        public virtual DbSet<tbl_jobexperiences> tbl_jobexperiences { get; set; }
        public virtual DbSet<tbl_listkala> tbl_listkala { get; set; }
        public virtual DbSet<tbl_listkala97> tbl_listkala97 { get; set; }
        public virtual DbSet<tbl_news> tbl_news { get; set; }
        public virtual DbSet<tbl_newscomments> tbl_newscomments { get; set; }
        public virtual DbSet<tbl_newsimagefiles> tbl_newsimagefiles { get; set; }
        public virtual DbSet<tbl_newsimages> tbl_newsimages { get; set; }
        public virtual DbSet<tbl_newstype> tbl_newstype { get; set; }
        public virtual DbSet<tbl_onlinechatmsg> tbl_onlinechatmsg { get; set; }
        public virtual DbSet<tbl_order> tbl_order { get; set; }
        public virtual DbSet<tbl_orderdetails> tbl_orderdetails { get; set; }
        public virtual DbSet<tbl_Pcontact> tbl_Pcontact { get; set; }
        public virtual DbSet<tbl_person> tbl_person { get; set; }
        public virtual DbSet<tbl_PersonAddress> tbl_PersonAddress { get; set; }
        public virtual DbSet<tbl_PersonInformationSetting> tbl_PersonInformationSetting { get; set; }
        public virtual DbSet<tbl_products> tbl_products { get; set; }
        public virtual DbSet<tbl_purchasekart> tbl_purchasekart { get; set; }
        public virtual DbSet<tbl_purchasekartitemlist> tbl_purchasekartitemlist { get; set; }
        public virtual DbSet<tbl_sample> tbl_sample { get; set; }
        public virtual DbSet<tbl_samples> tbl_samples { get; set; }
        public virtual DbSet<tbl_searchproducts> tbl_searchproducts { get; set; }
        public virtual DbSet<tbl_section> tbl_section { get; set; }
        public virtual DbSet<tbl_setting> tbl_setting { get; set; }
        public virtual DbSet<tbl_signalRmsg> tbl_signalRmsg { get; set; }
        public virtual DbSet<tbl_signalrUsers> tbl_signalrUsers { get; set; }
        public virtual DbSet<tbl_skillappinfo> tbl_skillappinfo { get; set; }
        public virtual DbSet<tbl_skills> tbl_skills { get; set; }
        public virtual DbSet<tbl_slides> tbl_slides { get; set; }
        public virtual DbSet<tbl_statistics> tbl_statistics { get; set; }
        public virtual DbSet<tbl_supporterinonlinechat> tbl_supporterinonlinechat { get; set; }
        public virtual DbSet<tbl_tbl_onlineusers> tbl_tbl_onlineusers { get; set; }
        public virtual DbSet<tbl_transaction> tbl_transaction { get; set; }
        public virtual DbSet<tbl_transportaiondetails> tbl_transportaiondetails { get; set; }
        public virtual DbSet<tbl_transportationcost> tbl_transportationcost { get; set; }
        public virtual DbSet<tbl_transportationdeliverinfo> tbl_transportationdeliverinfo { get; set; }
        public virtual DbSet<tbl_user> tbl_user { get; set; }
        public virtual DbSet<tbl_usermoreinfo> tbl_usermoreinfo { get; set; }
        public virtual DbSet<tbl_vehicletype> tbl_vehicletype { get; set; }
        public virtual DbSet<tbl_sentMessag> tbl_sentMessag { get; set; }
        public virtual DbSet<tbl_SentMessagPerson> tbl_SentMessagPerson { get; set; }
        public virtual DbSet<tbl_templateMessages> tbl_TemplateMessages { get; set; }
        public virtual DbSet<tbl_GroupPersons> tbl_GroupPersons { get; set; }
        public virtual DbSet<tbl_Groups> tbl_Groups { get; set; }
        public virtual DbSet<tbl_purchasecartSignalR> tbl_purchasecartSignalR { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                   string stringconnection = "Server=185.88.153.157,1430; database=4820_soltaniweb;persist security info=True;user id=4820_alireza;password=82191108219110 ";

                optionsBuilder.UseLazyLoadingProxies().UseSqlServer(stringconnection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:DefaultSchema", "4820_alireza");

            modelBuilder.Entity<tbl_accesslevels>(entity =>
            {
                entity.ToTable("tbl_accesslevels", "11004_user1");

                entity.HasOne(d => d.access)
                    .WithMany(p => p.tbl_accesslevels)
                    .HasForeignKey(d => d.accessid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_accesslevels_tbl_accesstypes");

                entity.HasOne(d => d.user)
                    .WithMany(p => p.tbl_accesslevels)
                    .HasForeignKey(d => d.userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_accesslevels_tbl_user");
            });

            modelBuilder.Entity<tbl_accesstypes>(entity =>
            {
                entity.ToTable("tbl_accesstypes", "11004_user1");

                entity.HasOne(d => d.actiontypeNavigation)
                    .WithMany(p => p.tbl_accesstypes)
                    .HasForeignKey(d => d.actiontype)
                    .HasConstraintName("FK_tbl_accesstypes_tbl_actionaccesstype");
            });

            modelBuilder.Entity<tbl_actionaccessibility>(entity =>
            {
                entity.ToTable("tbl_actionaccessibility", "11004_user1");

                entity.HasOne(d => d.acction_)
                    .WithMany(p => p.tbl_actionaccessibility)
                    .HasForeignKey(d => d.acction_id)
                    .HasConstraintName("FK_tbl_actionaccessibility_D5CF25C8");

                entity.HasOne(d => d.user)
                    .WithMany(p => p.tbl_actionaccessibility)
                    .HasForeignKey(d => d.userid)
                    .HasConstraintName("FK_tbl_actionaccessibility_59F80EA5");
            });

            modelBuilder.Entity<tbl_actionaccesstype>(entity =>
            {
                entity.ToTable("tbl_actionaccesstype", "11004_user1");

                entity.Property(e => e.actionaccesstype).HasMaxLength(100);

                entity.Property(e => e.description).HasMaxLength(100);
            });

            modelBuilder.Entity<tbl_actions>(entity =>
            {
                entity.ToTable("tbl_actions", "11004_user1");

                entity.HasOne(d => d.controller_)
                    .WithMany(p => p.tbl_actions)
                    .HasForeignKey(d => d.controller_id)
                    .HasConstraintName("FK_tbl_actions_99B67FB8");

                entity.HasOne(d => d.menu_)
                    .WithMany(p => p.tbl_actions)
                    .HasForeignKey(d => d.menu_id)
                    .HasConstraintName("FK_tbl_actions_950E29DF");
            });

            modelBuilder.Entity<tbl_anbar>(entity =>
            {
                entity.ToTable("tbl_anbar", "11004_user1");

                entity.Property(e => e.anbarname)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<tbl_appcontactsinfo>(entity =>
            {
                entity.ToTable("tbl_appcontactsinfo", "11004_user1");

                entity.HasOne(d => d.applicant_)
                    .WithMany(p => p.tbl_appcontactsinfo)
                    .HasForeignKey(d => d.applicant_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_appcontactsinfo_tbl_applicants");

                entity.HasOne(d => d.contacttype_)
                    .WithMany(p => p.tbl_appcontactsinfo)
                    .HasForeignKey(d => d.contacttype_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_appcontactsinfo_tbl_contactype");
            });

            modelBuilder.Entity<tbl_applicantrelativeinfo>(entity =>
            {
                entity.ToTable("tbl_applicantrelativeinfo", "11004_user1");

                entity.HasOne(d => d.applicant_)
                    .WithMany(p => p.tbl_applicantrelativeinfo)
                    .HasForeignKey(d => d.applicant_id)
                    .HasConstraintName("FK_tbl_applicantrelativeinfo_tbl_applicants");
            });

            modelBuilder.Entity<tbl_applicants>(entity =>
            {
                entity.HasKey(e => e.applicant_id);

                entity.ToTable("tbl_applicants", "11004_user1");

                entity.Property(e => e.applicant_image).HasColumnType("image");

                entity.Property(e => e.birthday).HasColumnType("datetime");

                entity.Property(e => e.cellnumber).IsRequired();

                entity.Property(e => e.codemelli).IsRequired();

                entity.Property(e => e.sabtdate).HasColumnType("datetime");

                entity.HasOne(d => d.degree_)
                    .WithMany(p => p.tbl_applicants)
                    .HasForeignKey(d => d.degree_id)
                    .HasConstraintName("FK_tbl_applicants_tbl_educationaldegree");

                entity.HasOne(d => d.job_)
                    .WithMany(p => p.tbl_applicants)
                    .HasForeignKey(d => d.job_id)
                    .HasConstraintName("FK_tbl_applicants_tbl_job");
            });

            modelBuilder.Entity<tbl_category>(entity =>
            {
                entity.ToTable("tbl_category", "dbo");

                entity.HasIndex(e => e.categoryname)
                    .HasName("IX_tbl_category")
                    .IsUnique();

                entity.Property(e => e.categoryname)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.image).HasColumnType("image");

                entity.Property(e => e.imagecategory).HasMaxLength(50);

                entity.Property(e => e.shortname).HasMaxLength(20);

                entity.HasOne(d => d.section_)
                    .WithMany(p => p.tbl_category)
                    .HasForeignKey(d => d.section_id)
                    .HasConstraintName("FK_tbl_category_tbl_section");
            });

            modelBuilder.Entity<tbl_contactus>(entity =>
            {
                entity.ToTable("tbl_contactus", "11004_user1");

                entity.Property(e => e.ip).HasMaxLength(100);

                entity.Property(e => e.name).HasMaxLength(100);

                entity.Property(e => e.sabtdate).HasColumnType("datetime");
            });

            modelBuilder.Entity<tbl_contactype>(entity =>
            {
                entity.ToTable("tbl_contactype", "11004_user1");

                entity.Property(e => e.contacttype).IsRequired();
            });

            modelBuilder.Entity<tbl_controllers>(entity =>
            {
                entity.ToTable("tbl_controllers", "11004_user1");
            });

            modelBuilder.Entity<tbl_DecorType>(entity =>
            {
                entity.ToTable("tbl_DecorType", "11004_user1");

                entity.Property(e => e.name).IsRequired();
            });

            modelBuilder.Entity<tbl_discount>(entity =>
            {
                entity.ToTable("tbl_discount", "11004_user1");

                entity.Property(e => e.discountcode).HasMaxLength(50);

                entity.Property(e => e.enddate).HasColumnType("datetime");

                entity.Property(e => e.startdate).HasColumnType("datetime");
            });

            modelBuilder.Entity<tbl_educationaldegree>(entity =>
            {
                entity.HasKey(e => e.educationid);

                entity.ToTable("tbl_educationaldegree", "11004_user1");

                entity.Property(e => e.degree).HasMaxLength(500);
            });

            modelBuilder.Entity<tbl_educationalrecords>(entity =>
            {
                entity.ToTable("tbl_educationalrecords", "11004_user1");

                entity.Property(e => e.enddate).HasColumnType("datetime");

                entity.Property(e => e.score).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.startdate).HasColumnType("datetime");

                entity.HasOne(d => d.applicant_)
                    .WithMany(p => p.tbl_educationalrecords)
                    .HasForeignKey(d => d.applicant_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_educationalrecords_tbl_applicants");

                entity.HasOne(d => d.degree_)
                    .WithMany(p => p.tbl_educationalrecords)
                    .HasForeignKey(d => d.degree_id)
                    .HasConstraintName("FK_tbl_educationalrecords_tbl_educationaldegree");
            });

            modelBuilder.Entity<tbl_files>(entity =>
            {
                entity.ToTable("tbl_files", "11004_user1");

                entity.Property(e => e.date).HasColumnType("datetime");

                entity.Property(e => e.filename)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.decortype)
                    .WithMany(p => p.tbl_files)
                    .HasForeignKey(d => d.decortypeid)
                    .HasConstraintName("FK_tbl_files_tbl_DecorType");

                entity.HasOne(d => d.ftp)
                    .WithMany(p => p.tbl_files)
                    .HasForeignKey(d => d.ftpid)
                    .HasConstraintName("FK_tbl_files_tbl_files");

                entity.HasOne(d => d.madeby)
                    .WithMany(p => p.tbl_files)
                    .HasForeignKey(d => d.madebyid)
                    .HasConstraintName("FK_tbl_files_tbl_person");

                entity.HasOne(d => d.user)
                    .WithMany(p => p.tbl_files)
                    .HasForeignKey(d => d.userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_files_tbl_user");
            });

            modelBuilder.Entity<tbl_filestosend>(entity =>
            {
                entity.Property(e => e.uploaddatetime).HasColumnType("datetime");

                entity.HasOne(d => d.ftp_)
                    .WithMany(p => p.tbl_filestosend)
                    .HasForeignKey(d => d.ftp_id)
                    .HasConstraintName("FK_tbl_filestosend_tbl_ftp");

                entity.HasOne(d => d.user_)
                    .WithMany(p => p.tbl_filestosend)
                    .HasForeignKey(d => d.user_id)
                    .HasConstraintName("FK_tbl_filestosend_tbl_user");
            });

            modelBuilder.Entity<tbl_ftp>(entity =>
            {
                entity.ToTable("tbl_ftp", "11004_user1");
            });

            modelBuilder.Entity<tbl_havaletype>(entity =>
            {
                entity.HasKey(e => e.htype);

                entity.ToTable("tbl_havaletype", "11004_user1");

                entity.Property(e => e.htype).ValueGeneratedNever();

                entity.Property(e => e.name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.htypeNavigation)
                    .WithOne(p => p.InversehtypeNavigation)
                    .HasForeignKey<tbl_havaletype>(d => d.htype)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_havaletype_havaletype");
            });

            modelBuilder.Entity<tbl_historyappconnect>(entity =>
            {
                entity.ToTable("tbl_historyappconnect", "11004_user1");

                entity.Property(e => e.sabtdate).HasColumnType("datetime");

                entity.HasOne(d => d.app_)
                    .WithMany(p => p.tbl_historyappconnect)
                    .HasForeignKey(d => d.app_id)
                    .HasConstraintName("FK_tbl_historyappconnect_tbl_applicants");

                entity.HasOne(d => d.user)
                    .WithMany(p => p.tbl_historyappconnect)
                    .HasForeignKey(d => d.userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_historyappconnect_tbl_user");
            });

            modelBuilder.Entity<tbl_job>(entity =>
            {
                entity.HasKey(e => e.jobid);

                entity.ToTable("tbl_job", "11004_user1");

                entity.Property(e => e.jobdesc).HasMaxLength(500);

                entity.Property(e => e.jobtitle).HasMaxLength(100);
            });

            modelBuilder.Entity<tbl_jobexperiences>(entity =>
            {
                entity.ToTable("tbl_jobexperiences", "11004_user1");

                entity.Property(e => e.enddate).HasColumnType("datetime");

                entity.Property(e => e.startdate).HasColumnType("datetime");

                entity.HasOne(d => d.applicant_)
                    .WithMany(p => p.tbl_jobexperiences)
                    .HasForeignKey(d => d.applicant_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_jobexperiences_tbl_applicants");
            });

            modelBuilder.Entity<tbl_listkala>(entity =>
            {
                entity.ToTable("tbl_listkala", "11004_user1");

                entity.Property(e => e.kalacost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.sodoordate).HasColumnType("datetime");

                entity.Property(e => e.time).HasMaxLength(50);

                entity.Property(e => e.totalcost).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.anbar)
                    .WithMany(p => p.tbl_listkala)
                    .HasForeignKey(d => d.anbarid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_listkala_tbl_anbar");

                entity.HasOne(d => d.htypeNavigation)
                    .WithMany(p => p.tbl_listkala)
                    .HasForeignKey(d => d.htype)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_listkala_havaletype");

                entity.HasOne(d => d.product)
                    .WithMany(p => p.tbl_listkala)
                    .HasForeignKey(d => d.productid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_listkala_tbl_products");

                entity.HasOne(d => d.username)
                    .WithMany(p => p.tbl_listkala)
                    .HasForeignKey(d => d.usernameid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_listkala_tbl_user");
            });

            modelBuilder.Entity<tbl_listkala97>(entity =>
            {
                entity.ToTable("tbl_listkala97", "11004_user1");

                entity.Property(e => e.kalacost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.sodoordate).HasColumnType("datetime");

                entity.Property(e => e.time).HasMaxLength(50);

                entity.Property(e => e.totalcost).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.anbar)
                    .WithMany(p => p.tbl_listkala97)
                    .HasForeignKey(d => d.anbarid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_listkala97_tbl_anbar");

                entity.HasOne(d => d.htypeNavigation)
                    .WithMany(p => p.tbl_listkala97)
                    .HasForeignKey(d => d.htype)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_listkala97_havaletype");

                entity.HasOne(d => d.product)
                    .WithMany(p => p.tbl_listkala97)
                    .HasForeignKey(d => d.productid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_listkala97_tbl_products");

                entity.HasOne(d => d.username)
                    .WithMany(p => p.tbl_listkala97)
                    .HasForeignKey(d => d.usernameid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_listkala97_tbl_user");
            });

            modelBuilder.Entity<tbl_news>(entity =>
            {
                entity.ToTable("tbl_news", "11004_user1");

                entity.Property(e => e.issudate).HasColumnType("datetime");

                entity.Property(e => e.title).HasMaxLength(150);

                entity.Property(e => e.writedate).HasColumnType("datetime");

                entity.HasOne(d => d.newtype)
                    .WithMany(p => p.tbl_news)
                    .HasForeignKey(d => d.newtypeid)
                    .HasConstraintName("FK_tbl_news_tbl_newstype");

                entity.HasOne(d => d.user)
                    .WithMany(p => p.tbl_news)
                    .HasForeignKey(d => d.userid)
                    .HasConstraintName("FK_tbl_news_tbl_user");
            });

            modelBuilder.Entity<tbl_newscomments>(entity =>
            {
                entity.ToTable("tbl_newscomments", "11004_user1");

                entity.Property(e => e.datecomment).HasColumnType("datetime");

                entity.Property(e => e.ip)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.name).HasMaxLength(100);

                entity.Property(e => e.text)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.news)
                    .WithMany(p => p.tbl_newscomments)
                    .HasForeignKey(d => d.newsid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_newscomments_tbl_news");
            });

            modelBuilder.Entity<tbl_newsimagefiles>(entity =>
            {
                entity.ToTable("tbl_newsimagefiles", "11004_user1");

                entity.Property(e => e.date).HasColumnType("datetime");

                entity.HasOne(d => d.ftp)
                    .WithMany(p => p.tbl_newsimagefiles)
                    .HasForeignKey(d => d.ftpid)
                    .HasConstraintName("FK_tbl_newsimagefile_tbl_ftp");

                entity.HasOne(d => d.user)
                    .WithMany(p => p.tbl_newsimagefiles)
                    .HasForeignKey(d => d.userid)
                    .HasConstraintName("FK_tbl_newsimagefiles_tbl_user");
            });

            modelBuilder.Entity<tbl_newsimages>(entity =>
            {
                entity.ToTable("tbl_newsimages", "11004_user1");

                entity.HasOne(d => d.news)
                    .WithMany(p => p.tbl_newsimages)
                    .HasForeignKey(d => d.newsid)
                    .HasConstraintName("FK_tbl_newsimages_tbl_news");

                entity.HasOne(d => d.newsimage)
                    .WithMany(p => p.tbl_newsimages)
                    .HasForeignKey(d => d.newsimageid)
                    .HasConstraintName("FK_tbl_newsimages_tbl_newsimagefiles");

                entity.HasOne(d => d.user)
                    .WithMany(p => p.tbl_newsimages)
                    .HasForeignKey(d => d.userid)
                    .HasConstraintName("FK_tbl_newsimages_tbl_user");
            });

            modelBuilder.Entity<tbl_newstype>(entity =>
            {
                entity.ToTable("tbl_newstype", "11004_user1");
            });

            modelBuilder.Entity<tbl_onlinechatmsg>(entity =>
            {
                entity.Property(e => e.sendmsg_date).HasColumnType("datetime");

                entity.Property(e => e.textmsg).IsRequired();

                entity.HasOne(d => d.onlineuser_)
                    .WithMany(p => p.tbl_onlinechatmsgonlineuser_)
                    .HasForeignKey(d => d.onlineuser_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_onlinechatmsg_tbl_tbl_onlineusers");

                entity.HasOne(d => d.supporter_)
                    .WithMany(p => p.tbl_onlinechatmsgsupporter_)
                    .HasForeignKey(d => d.supporter_id)
                    .HasConstraintName("FK_tbl_onlinechatmsg_tbl_tbl_onlineusers1");

                entity.HasOne(d => d.towhomNavigation)
                    .WithMany(p => p.tbl_onlinechatmsgtowhomNavigation)
                    .HasForeignKey(d => d.towhom)
                    .HasConstraintName("FK_tbl_onlinechatmsg_tbl_tbl_onlineusers2");
            });

            modelBuilder.Entity<tbl_order>(entity =>
            {
                entity.Property(e => e.done_datetime).HasColumnType("datetime");

                entity.Property(e => e.sodoor_date).HasColumnType("datetime");

                entity.HasOne(d => d.done_user)
                    .WithMany(p => p.tbl_orderdone_user)
                    .HasForeignKey(d => d.done_userid)
                    .HasConstraintName("FK_tbl_order_tbl_user1");

                entity.HasOne(d => d.from_branch)
                    .WithMany(p => p.tbl_orderfrom_branch)
                    .HasForeignKey(d => d.from_branchid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_order_tbl_branches");

                entity.HasOne(d => d.orderfor)
                    .WithMany(p => p.tbl_order)
                    .HasForeignKey(d => d.orderforid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_order_tbl_person");

                entity.HasOne(d => d.to_branch)
                    .WithMany(p => p.tbl_orderto_branch)
                    .HasForeignKey(d => d.to_branchid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_order_tbl_branches1");

                entity.HasOne(d => d.user)
                    .WithMany(p => p.tbl_orderuser)
                    .HasForeignKey(d => d.userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_order_tbl_user");
            });

            modelBuilder.Entity<tbl_orderdetails>(entity =>
            {
                entity.HasOne(d => d.order_)
                    .WithMany(p => p.tbl_orderdetails)
                    .HasForeignKey(d => d.order_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_orderdetails_tbl_order");

                entity.HasOne(d => d.product_)
                    .WithMany(p => p.tbl_orderdetails)
                    .HasForeignKey(d => d.product_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_orderdetails_tbl_products");
            });

            modelBuilder.Entity<tbl_Pcontact>(entity =>
            {
                entity.ToTable("tbl_Pcontact", "dbo");

                entity.HasIndex(e => e.idname)
                    .HasName("IX_tbl_Pcontact");

                entity.Property(e => e.cell).HasMaxLength(50);

                entity.Property(e => e.tel).HasMaxLength(50);

                entity.HasOne(d => d.idnameNavigation)
                    .WithMany(p => p.tbl_Pcontact)
                    .HasForeignKey(d => d.idname)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Pcontact_tbl_person");
            });

            modelBuilder.Entity<tbl_person>(entity =>
            {
                entity.ToTable("tbl_person", "dbo");

                entity.HasIndex(e => e.Fname)
                    .HasName("IX_tbl_person");

                entity.Property(e => e.Fname)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.HasOne(d => d.Branches)
                    .WithMany(p => p.Persons)
                    .HasForeignKey(d => d.BrancheId)
                    .HasConstraintName("FK_tbl_CompanySection_tbl_Company");

                entity.Property(e => e.Lname).HasMaxLength(100);

                entity.Property(e => e.birthday).HasColumnType("date");

                entity.Property(e => e.cell).HasMaxLength(100);

                entity.Property(e => e.codemelli).HasMaxLength(50);

                entity.Property(e => e.job).HasMaxLength(100);

                entity.Property(e => e.sex).HasMaxLength(50);

                entity.Property(e => e.tell).HasMaxLength(100);
            });
            modelBuilder.Entity<tbl_CompanySection>(entity =>
            {
                entity.ToTable("tbl_CompanySection", "4820_alireza");
                entity.HasOne(d => d.tbl_section)
                    .WithMany(p => p.tbl_CompanySection)
                    .HasForeignKey(d => d.SectionId)
                    .HasConstraintName("FK_tbl_CompanySection_tbl_section");
                entity.HasOne(d => d.tbl_Company)
                    .WithMany(p => p.tbl_CompanySection)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_tbl_CompanySection_tbl_Company");
            });
            modelBuilder.Entity<tbl_CompanyPerson>(entity =>
            {
                entity.ToTable("tbl_CompanyPerson", "4820_alireza");
                entity.HasOne(d => d.tbl_person)
                    .WithMany(p => p.tbl_CompanyPerson)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_tbl_CompanyPerson_tbl_person");
                entity.HasOne(d => d.tbl_Company)
                    .WithMany(p => p.tbl_CompanyPerson)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_tbl_CompanyPerson_tbl_Company");
            });
            modelBuilder.Entity<tbl_Company>(entity =>
            {
                entity.ToTable("tbl_Company", "4820_alireza");

            });
            modelBuilder.Entity<tbl_PersonAddress>(entity =>
            {
                entity.ToTable("tbl_PersonAddress", "4820_alireza");
                entity.HasOne(d => d.tbl_person)
                    .WithMany(p => p.PersonAddresses)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_tbl_PersonAddress_tbl_Person");
            });
            modelBuilder.Entity<tbl_PersonInformationSetting>(entity =>
            {
                entity.ToTable("tbl_PersonInformationSetting", "4820_alireza");
                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonInformationSettings)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_tbl_PersonInformationSetting_tbl_Person");
            });

            modelBuilder.Entity<tbl_sentMessag>(entity =>
            {
                entity.ToTable("tbl_sentMessag", "4820_alireza");
              
                entity.HasOne(d => d.User)
                    .WithMany(p => p.tbl_sentMessag)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_tbl_sentMessag_tbl_user");
            });
            modelBuilder.Entity<tbl_templateMessages>(entity =>
            {
                entity.ToTable("tbl_templateMessages", "4820_alireza");

              
            });
            modelBuilder.Entity<tbl_Groups>(entity =>
            {
                entity.ToTable("tbl_Groups", "4820_alireza");


            });
            modelBuilder.Entity<tbl_GroupPersons>(entity =>
            {
                entity.ToTable("tbl_GroupPersons", "4820_alireza");

                entity
                     .HasKey(bc => new { bc.PersonId, bc.GroupId });
                entity
                     .HasOne(bc => bc.Group)
                    .WithMany(b => b.Persons)
                    .HasForeignKey(bc => bc.GroupId)
                    .HasConstraintName("FK_tbl_GroupPersons_tbl_Group");

                entity
                     .HasOne(bc => bc.Person)
                    .WithMany(c => c.Groups)
                    .HasForeignKey(bc => bc.PersonId)
                    .HasConstraintName("FK_tbl_GroupPersons_tbl_person");
            });
            modelBuilder.Entity<tbl_SentMessagPerson>(entity =>
            {
                entity.ToTable("tbl_SentMessagPerson", "4820_alireza");
              
                entity.HasOne(d => d.SentMessag)
                    .WithMany(p => p.SentMessagPersones)
                    .HasForeignKey(d => d.SentMessagId)
                    .HasConstraintName("FK_tbl_SentMessagPerson_tbl_sentMessag");
                entity.HasOne(d => d.Person)
                    .WithMany(p => p.tbl_SentMessagPerson)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_tbl_SentMessagPerson_tbl_Person");
            });

            modelBuilder.Entity<tbl_products>(entity =>
            {
                entity.HasKey(e => e.idproduct);

                entity.ToTable("tbl_products", "dbo");

                entity.HasIndex(e => e.grade)
                    .HasName("IX_tbl_products_2");

                entity.HasIndex(e => e.name)
                    .HasName("IX_tbl_products_1");

                entity.Property(e => e.codename).HasMaxLength(50);

                entity.Property(e => e.date).HasColumnType("datetime");

                entity.Property(e => e.grade).HasMaxLength(100);

                entity.Property(e => e.image).HasColumnType("image");

                entity.Property(e => e.imageproducts).HasMaxLength(50);

                entity.Property(e => e.lastbuycost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.lastcellcost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.thumbnail_image).HasColumnType("image");

                entity.Property(e => e.updatebuycost).HasColumnType("datetime");

                entity.Property(e => e.updatesellcost).HasColumnType("datetime");

                entity.Property(e => e.weight).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.category)
                    .WithMany(p => p.tbl_products)
                    .HasForeignKey(d => d.categoryid)
                    .HasConstraintName("FK_products_category");

                entity.HasOne(d => d.ftp)
                    .WithMany(p => p.tbl_products)
                    .HasForeignKey(d => d.ftpid)
                    .HasConstraintName("FK_tbl_products_tbl_ftp");
            });

            modelBuilder.Entity<tbl_purchasekart>(entity =>
            {
                entity.ToTable("tbl_purchasekart", "11004_user1");

                entity.Property(e => e.purchasedateend).HasColumnType("datetime");

                entity.Property(e => e.purchasedatestart).HasColumnType("datetime");

                entity.Property(e => e.transId).HasMaxLength(10);

                entity.HasOne(d => d.discount_)
                    .WithMany(p => p.tbl_purchasekart)
                    .HasForeignKey(d => d.discount_id)
                    .HasConstraintName("FK_tbl_purchasekart_tbl_discount");

                entity.HasOne(d => d.user)
                    .WithMany(p => p.tbl_purchasekart)
                    .HasForeignKey(d => d.userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_purchasekart_tbl_user");
            });

            modelBuilder.Entity<tbl_purchasekartitemlist>(entity =>
            {
                entity.ToTable("tbl_purchasekartitemlist", "11004_user1");

                entity.Property(e => e.price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.purchase_datetime).HasColumnType("datetime");

                entity.Property(e => e.totalprice).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.perchasekart_)
                    .WithMany(p => p.tbl_purchasekartitemlist)
                    .HasForeignKey(d => d.perchasekart_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_purchasekartitemlist_tbl_purchasekartitemlist");

                entity.HasOne(d => d.product_)
                    .WithMany(p => p.tbl_purchasekartitemlist)
                    .HasForeignKey(d => d.product_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_purchasekartitemlist_tbl_products");
            });

            modelBuilder.Entity<tbl_sample>(entity =>
            {
                entity.HasKey(e => e.idsample);

                entity.ToTable("tbl_sample", "dbo");

                entity.HasIndex(e => e.codename)
                    .HasName("IX_sample_1");

                entity.HasIndex(e => e.namesample)
                    .HasName("IX_sample");

                entity.Property(e => e.idsample).ValueGeneratedOnAdd();

                entity.Property(e => e.codename)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.namesample)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.sampleimg).IsRequired();

                entity.HasOne(d => d.idcategoryNavigation)
                    .WithMany(p => p.tbl_sample)
                    .HasForeignKey(d => d.idcategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_sample_tbl_category");

                entity.HasOne(d => d.idsampleNavigation)
                    .WithOne(p => p.InverseidsampleNavigation)
                    .HasForeignKey<tbl_sample>(d => d.idsample)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_sample_tbl_sample");
            });

            modelBuilder.Entity<tbl_samples>(entity =>
            {
                entity.ToTable("tbl_samples", "11004_user1");

                entity.HasIndex(e => e.productsid)
                    .HasName("IX_tbl_samples-products");

                entity.Property(e => e.date).HasColumnType("datetime");

                entity.HasOne(d => d.files)
                    .WithMany(p => p.tbl_samples)
                    .HasForeignKey(d => d.filesid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_samples_samples");

                entity.HasOne(d => d.products)
                    .WithMany(p => p.tbl_samples)
                    .HasForeignKey(d => d.productsid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_samples_tbl_products");

                entity.HasOne(d => d.user)
                    .WithMany(p => p.tbl_samples)
                    .HasForeignKey(d => d.userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_samples_tbl_user1");
            });

            modelBuilder.Entity<tbl_searchproducts>(entity =>
            {
                entity.ToTable("tbl_searchproducts", "11004_user1");

                entity.Property(e => e.ip)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.searchdate).HasColumnType("datetime");

                entity.Property(e => e.searchtext).IsRequired();

                entity.HasOne(d => d.user_)
                    .WithMany(p => p.tbl_searchproducts)
                    .HasForeignKey(d => d.user_id)
                    .HasConstraintName("FK_tbl_searchproducts_tbl_user");
            });

            modelBuilder.Entity<tbl_section>(entity =>
            {
                entity.ToTable("tbl_section", "11004_user1");

                entity.Property(e => e.image).HasMaxLength(50);

                entity.Property(e => e.shortname).HasMaxLength(50);

                entity.HasOne(d => d.ftp)
                    .WithMany(p => p.tbl_section)
                    .HasForeignKey(d => d.ftpid)
                    .HasConstraintName("FK_tbl_section_tbl_ftp");
            });

            modelBuilder.Entity<tbl_setting>(entity =>
            {
                entity.ToTable("tbl_setting", "11004_user1");
            });

            modelBuilder.Entity<tbl_signalRmsg>(entity =>
            {
                entity.Property(e => e.datetime_pin).HasColumnType("datetime");

                entity.Property(e => e.datetime_read).HasColumnType("datetime");

                entity.Property(e => e.datetime_send).HasColumnType("datetime");

                entity.Property(e => e.files).HasMaxLength(5000);

                entity.HasOne(d => d.from_user)
                    .WithMany(p => p.tbl_signalRmsgfrom_user)
                    .HasForeignKey(d => d.from_userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_signalRmsg_tbl_user1");

                entity.HasOne(d => d.to_user)
                    .WithMany(p => p.tbl_signalRmsgto_user)
                    .HasForeignKey(d => d.to_userid)
                    .HasConstraintName("FK_tbl_signalRmsg_tbl_user");
            });

            modelBuilder.Entity<tbl_signalrUsers>(entity =>
            {
                entity.Property(e => e.connectionId).HasMaxLength(50);

                entity.Property(e => e.fullname).HasMaxLength(50);

                entity.Property(e => e.username).HasMaxLength(50);

                entity.HasOne(d => d.user)
                    .WithMany(p => p.tbl_signalrUsers)
                    .HasForeignKey(d => d.userid)
                    .HasConstraintName("FK_tbl_signalrUsers_tbl_user");
            });

            modelBuilder.Entity<tbl_skillappinfo>(entity =>
            {
                entity.ToTable("tbl_skillappinfo", "11004_user1");

                entity.Property(e => e.skillvalue).IsRequired();

                entity.HasOne(d => d.app)
                    .WithMany(p => p.tbl_skillappinfo)
                    .HasForeignKey(d => d.appid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_skillappinfo_tbl_applicants");

                entity.HasOne(d => d.skill)
                    .WithMany(p => p.tbl_skillappinfo)
                    .HasForeignKey(d => d.skillid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_skillappinfo_tbl_skills");
            });

            modelBuilder.Entity<tbl_skills>(entity =>
            {
                entity.ToTable("tbl_skills", "11004_user1");

                entity.Property(e => e.skillname).IsRequired();
            });

            modelBuilder.Entity<tbl_slides>(entity =>
            {
                entity.HasKey(e => e.slideID);

                entity.ToTable("tbl_slides", "11004_user1");

                entity.Property(e => e.imge).HasColumnType("image");

                entity.Property(e => e.nameimage)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.nameslide)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.thumbnail_image).HasColumnType("image");

                entity.HasOne(d => d.ftp)
                    .WithMany(p => p.tbl_slides)
                    .HasForeignKey(d => d.ftpid)
                    .HasConstraintName("FK_tbl_slides_tbl_ftp");
            });

            modelBuilder.Entity<tbl_statistics>(entity =>
            {
                entity.ToTable("tbl_statistics", "11004_user1");

                entity.Property(e => e.actionname)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.controllername)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.enterdate).HasColumnType("datetime");

                entity.Property(e => e.ip)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.user_)
                    .WithMany(p => p.tbl_statistics)
                    .HasForeignKey(d => d.user_id)
                    .HasConstraintName("FK_tbl_statistics_tbl_user");
            });

            modelBuilder.Entity<tbl_supporterinonlinechat>(entity =>
            {
                entity.Property(e => e.supporte_name).HasMaxLength(50);

                entity.HasOne(d => d.user_)
                    .WithMany(p => p.tbl_supporterinonlinechat)
                    .HasForeignKey(d => d.user_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_supporterinonlinechat_tbl_user");
            });

            modelBuilder.Entity<tbl_tbl_onlineusers>(entity =>
            {
                entity.Property(e => e.connection_id).HasMaxLength(50);

                entity.Property(e => e.email).HasMaxLength(250);

                entity.Property(e => e.nameu).HasMaxLength(50);

                entity.HasOne(d => d.user_)
                    .WithMany(p => p.tbl_tbl_onlineusers)
                    .HasForeignKey(d => d.user_id)
                    .HasConstraintName("FK_tbl_tbl_onlineusers_tbl_user");
            });

            modelBuilder.Entity<tbl_transaction>(entity =>
            {
                entity.ToTable("tbl_transaction", "11004_user1");

                entity.Property(e => e.amount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.sharh).HasMaxLength(500);

                entity.Property(e => e.transid)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.varizdate).HasColumnType("datetime");

                entity.HasOne(d => d.cart)
                    .WithMany(p => p.tbl_transaction)
                    .HasForeignKey(d => d.cartid)
                    .HasConstraintName("FK_tbl_transaction_tbl_purchasekart");

                entity.HasOne(d => d.user_)
                    .WithMany(p => p.tbl_transaction)
                    .HasForeignKey(d => d.user_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_transaction_tbl_user");
            });

            modelBuilder.Entity<tbl_transportaiondetails>(entity =>
            {
                entity.ToTable("tbl_transportaiondetails", "11004_user1");

                entity.Property(e => e.distance).HasMaxLength(50);

                entity.Property(e => e.lat).HasMaxLength(50);

                entity.Property(e => e.lng).HasMaxLength(50);

                entity.Property(e => e.tell).HasMaxLength(50);

                entity.HasOne(d => d.cart)
                    .WithMany(p => p.tbl_transportaiondetails)
                    .HasForeignKey(d => d.cartid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_transportaiondetails_tbl_purchasekart");
            });

            modelBuilder.Entity<tbl_transportationcost>(entity =>
            {
                entity.ToTable("tbl_transportationcost", "11004_user1");

                entity.Property(e => e.distance).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.tcost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.totaltcost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.totalweight).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.cart_)
                    .WithMany(p => p.tbl_transportationcost)
                    .HasForeignKey(d => d.cart_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_transportationcost_tbl_purchasekart");

                entity.HasOne(d => d.vehicle_)
                    .WithMany(p => p.tbl_transportationcost)
                    .HasForeignKey(d => d.vehicle_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_transportationcost_tbl_vehicletype");
            });

            modelBuilder.Entity<tbl_transportationdeliverinfo>(entity =>
            {
                entity.ToTable("tbl_transportationdeliverinfo", "11004_user1");

                entity.Property(e => e.deliver_date).HasColumnType("datetime");

                entity.Property(e => e.receipt_img).HasColumnType("image");

                entity.HasOne(d => d.cart)
                    .WithMany(p => p.tbl_transportationdeliverinfo)
                    .HasForeignKey(d => d.cartid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_transportationdeliverinfo_tbl_purchasekart");
            });

            modelBuilder.Entity<tbl_user>(entity =>
            {
                entity.ToTable("tbl_user", "dbo");

                entity.HasIndex(e => e.username)
                    .HasName("IX_tbl_user")
                    .IsUnique();

                entity.Property(e => e.cellphone).HasMaxLength(50);

                entity.Property(e => e.confirmpass).HasMaxLength(100);

                entity.Property(e => e.email).IsRequired();

                entity.Property(e => e.image).HasDefaultValueSql("(N'user.jpg')");

                entity.Property(e => e.img).HasColumnType("image");

                entity.Property(e => e.password)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.signupdate).HasColumnType("datetime");

                entity.Property(e => e.username)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.branches_)
                    .WithMany(p => p.tbl_user)
                    .HasForeignKey(d => d.branches_id)
                    .HasConstraintName("FK_tbl_user_tbl_branches");

                entity.HasOne(d => d.ftp)
                    .WithMany(p => p.tbl_user)
                    .HasForeignKey(d => d.ftpid)
                    .HasConstraintName("FK_tbl_user_tbl_user");
            });

            modelBuilder.Entity<tbl_usermoreinfo>(entity =>
            {
                entity.ToTable("tbl_usermoreinfo", "11004_user1");

                entity.Property(e => e.birthday).HasColumnType("datetime");

                entity.Property(e => e.codemelli).HasMaxLength(10);

                entity.HasOne(d => d.user_)
                    .WithMany(p => p.tbl_usermoreinfo)
                    .HasForeignKey(d => d.user_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_usermoreinfo_tbl_user");
            });

            modelBuilder.Entity<tbl_vehicletype>(entity =>
            {
                entity.ToTable("tbl_vehicletype", "11004_user1");

                entity.Property(e => e.capacity).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.factor).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.mincost).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<tbl_purchasecartSignalR>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.HasOne(d => d.idNavigation)
                    .WithOne(p => p.tbl_purchasecartSignalR)
                    .HasForeignKey<tbl_purchasecartSignalR>(d => d.id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_purchasecartSignalR_tbl_user");
            });
        }
    }
}
