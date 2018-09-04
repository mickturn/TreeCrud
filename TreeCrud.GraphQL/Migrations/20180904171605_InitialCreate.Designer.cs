﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TreeCrud.DataLayer.Data;

namespace TreeCrud.GraphQL.Migrations
{
    [DbContext(typeof(AligaContext))]
    [Migration("20180904171605_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932");

            modelBuilder.Entity("TreeCrud.DataLayer.Models.Unitat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<string>("Label");

                    b.Property<int?>("ParentId");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("Unitats");
                });
#pragma warning restore 612, 618
        }
    }
}
