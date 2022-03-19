// ReSharper disable VirtualMemberCallInConstructor
namespace InteriorPlatform.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using InteriorPlatform.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.UserProjects = new HashSet<UserProject>();
            this.Inquires = new HashSet<Inquire>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int ProjectsCount => this.UserProjects.Count;

        // Relationships
        [ForeignKey(nameof(Town))]
        public int TownId { get; set; }

        public Town Town { get; set; }

        [ForeignKey(nameof(Position))]
        public int PositionId { get; set; }

        public Position Position { get; set; }

        [ForeignKey(nameof(Company))]
        public int CompanyId { get; set; }

        public Company Company { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        // Collections
        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<UserProject> UserProjects { get; set; }

        public virtual ICollection<Inquire> Inquires { get; set; }
    }
}
