using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace NexaSoft.Agro.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.CreateTable(
                name: "constantes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tipo_constante = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    clave = table.Column<int>(type: "integer", nullable: false),
                    valor = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    estado_constante = table.Column<int>(type: "integer", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_modificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_eliminacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_constantes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "consultoras",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre_consultora = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    direccion_consultora = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    representante_consultora = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ruc_consultora = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: true),
                    correo_organizacional = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    estado_consultora = table.Column<int>(type: "integer", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_modificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_eliminacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_consultoras", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "contadores",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    entidad = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    prefijo = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    valor_actual = table.Column<long>(type: "bigint", maxLength: 16, nullable: false),
                    agrupador = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    tipo_dato = table.Column<string>(type: "text", nullable: true),
                    valor_rpeticion = table.Column<int>(type: "integer", nullable: true),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_modificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_eliminacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contadores", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "menu_item_options",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    label = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    icon = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    route = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    parent_id = table.Column<Guid>(type: "uuid", nullable: true),
                    estado_menu = table.Column<int>(type: "integer", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_modificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_eliminacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_menu_item_options", x => x.id);
                    table.ForeignKey(
                        name: "fk_menu_item_options_menu_item_options_parent_id",
                        column: x => x.parent_id,
                        principalTable: "menu_item_options",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "organizaciones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre_organizacion = table.Column<string>(type: "character varying(220)", maxLength: 220, nullable: true),
                    contacto_organizacion = table.Column<string>(type: "character varying(220)", maxLength: 220, nullable: true),
                    telefono_contacto = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    sector_id = table.Column<int>(type: "integer", nullable: false),
                    estado_organizacion = table.Column<int>(type: "integer", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_modificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_eliminacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizaciones", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "permissions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    referencia_control = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_modificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_eliminacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_permissions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "refresh_tokens",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    token = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    expires = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    revoked = table.Column<bool>(type: "boolean", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_modificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_eliminacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_refresh_tokens", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_modificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_eliminacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ubigeos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    descripcion = table.Column<string>(type: "character varying(180)", maxLength: 180, nullable: true),
                    nivel = table.Column<int>(type: "integer", nullable: false),
                    padre_id = table.Column<Guid>(type: "uuid", nullable: true),
                    estado_ubigeo = table.Column<int>(type: "integer", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_modificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_eliminacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ubigeos", x => x.id);
                    table.ForeignKey(
                        name: "fk_ubigeos_ubigeos_padre_id",
                        column: x => x.padre_id,
                        principalTable: "ubigeos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_default = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_roles", x => new { x.user_id, x.role_id });
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_apellidos = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    user_nombres = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    nombre_completo = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    user_name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    password = table.Column<string>(type: "character varying(220)", maxLength: 220, nullable: true),
                    email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    user_dni = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    user_telefono = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    estado_user = table.Column<int>(type: "integer", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_modificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_eliminacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "colaboradores",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombres_colaborador = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    apellidos_colaborador = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    nombre_completo_colaborador = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    tipo_documento_id = table.Column<int>(type: "integer", nullable: false),
                    numero_documento_identidad = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    fecha_nacimiento = table.Column<DateOnly>(type: "date", nullable: true),
                    genero_colaborador_id = table.Column<int>(type: "integer", nullable: false),
                    estado_civil_colaborador_id = table.Column<int>(type: "integer", nullable: false),
                    direccion = table.Column<string>(type: "character varying(180)", maxLength: 180, nullable: true),
                    correo_electronico = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    telefono_movil = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    cargo_id = table.Column<int>(type: "integer", nullable: false),
                    departamento_id = table.Column<int>(type: "integer", nullable: false),
                    fecha_ingreso = table.Column<DateOnly>(type: "date", nullable: true),
                    salario = table.Column<decimal>(type: "numeric", nullable: true),
                    fecha_cese = table.Column<DateOnly>(type: "date", nullable: true),
                    comentarios = table.Column<string>(type: "text", nullable: true),
                    consultora_id = table.Column<Guid>(type: "uuid", nullable: false),
                    estado_colaborador = table.Column<int>(type: "integer", nullable: false),
                    user_name = table.Column<string>(type: "text", nullable: true),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_modificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_eliminacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_colaboradores", x => x.id);
                    table.ForeignKey(
                        name: "fk_colaboradores_consultora_consultora_id",
                        column: x => x.consultora_id,
                        principalTable: "consultoras",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "menu_roles",
                columns: table => new
                {
                    menu_item_option_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_menu_roles", x => new { x.menu_item_option_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_menu_roles_menu_item_options_menu_item_option_id",
                        column: x => x.menu_item_option_id,
                        principalTable: "menu_item_options",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "role_permissions",
                columns: table => new
                {
                    role_id = table.Column<Guid>(type: "uuid", nullable: false),
                    permission_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_permissions", x => new { x.role_id, x.permission_id });
                    table.ForeignKey(
                        name: "fk_role_permissions_permissions_permission_id",
                        column: x => x.permission_id,
                        principalTable: "permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "empresas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    razon_social = table.Column<string>(type: "character varying(220)", maxLength: 220, nullable: true),
                    ruc_empresa = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: true),
                    contacto_empresa = table.Column<string>(type: "character varying(220)", maxLength: 220, nullable: true),
                    telefono_contacto_empresa = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    departamento_empresa_id = table.Column<Guid>(type: "uuid", nullable: false),
                    provincia_empresa_id = table.Column<Guid>(type: "uuid", nullable: false),
                    distrito_empresa_id = table.Column<Guid>(type: "uuid", nullable: false),
                    direccion = table.Column<string>(type: "character varying(220)", maxLength: 220, nullable: true),
                    latitud_empresa = table.Column<double>(type: "double precision", nullable: false),
                    longitud_empresa = table.Column<double>(type: "double precision", nullable: false),
                    organizacion_id = table.Column<Guid>(type: "uuid", nullable: false),
                    estado_empresa = table.Column<int>(type: "integer", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_modificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_eliminacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_empresas", x => x.id);
                    table.ForeignKey(
                        name: "fk_empresas_organizacion_organizacion_id",
                        column: x => x.organizacion_id,
                        principalTable: "organizaciones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_empresas_ubigeo_departamento_empresa_id",
                        column: x => x.departamento_empresa_id,
                        principalTable: "ubigeos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_empresas_ubigeo_distrito_empresa_id",
                        column: x => x.distrito_empresa_id,
                        principalTable: "ubigeos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_empresas_ubigeo_provincia_empresa_id",
                        column: x => x.provincia_empresa_id,
                        principalTable: "ubigeos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "estudios_ambientales",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    proyecto = table.Column<string>(type: "character varying(220)", maxLength: 220, nullable: false),
                    codigo_estudio = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    fecha_inicio = table.Column<DateOnly>(type: "date", nullable: false),
                    fecha_fin = table.Column<DateOnly>(type: "date", nullable: false),
                    detalles = table.Column<string>(type: "text", nullable: true),
                    empresa_id = table.Column<Guid>(type: "uuid", nullable: false),
                    estado_estudio_ambiental = table.Column<int>(type: "integer", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_modificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_eliminacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_estudios_ambientales", x => x.id);
                    table.ForeignKey(
                        name: "fk_estudios_ambientales_empresas_empresa_id",
                        column: x => x.empresa_id,
                        principalTable: "empresas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "capitulos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre_capitulo = table.Column<string>(type: "character varying(220)", maxLength: 220, nullable: true),
                    descripcion_capitulo = table.Column<string>(type: "character varying(550)", maxLength: 550, nullable: true),
                    estado_capitulo = table.Column<int>(type: "integer", nullable: false),
                    estudio_ambiental_id = table.Column<Guid>(type: "uuid", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_modificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_eliminacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_capitulos", x => x.id);
                    table.ForeignKey(
                        name: "fk_capitulos_estudio_ambiental_estudio_ambiental_id",
                        column: x => x.estudio_ambiental_id,
                        principalTable: "estudios_ambientales",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "subcapitulos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre_sub_capitulo = table.Column<string>(type: "character varying(220)", maxLength: 220, nullable: true),
                    descripcion_sub_capitulo = table.Column<string>(type: "character varying(550)", maxLength: 550, nullable: true),
                    capitulo_id = table.Column<Guid>(type: "uuid", nullable: false),
                    estado_sub_capitulo = table.Column<int>(type: "integer", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_modificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_eliminacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_subcapitulos", x => x.id);
                    table.ForeignKey(
                        name: "fk_subcapitulos_capitulos_capitulo_id",
                        column: x => x.capitulo_id,
                        principalTable: "capitulos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "estructuras",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tipo_estructura_id = table.Column<int>(type: "integer", nullable: false),
                    nombre_estructura = table.Column<string>(type: "character varying(220)", maxLength: 220, nullable: true),
                    descripcion_estructura = table.Column<string>(type: "character varying(550)", maxLength: 550, nullable: false),
                    padre_estructura_id = table.Column<Guid>(type: "uuid", nullable: true),
                    sub_capitulo_id = table.Column<Guid>(type: "uuid", nullable: false),
                    estado_estructura = table.Column<int>(type: "integer", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_modificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_eliminacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_estructuras", x => x.id);
                    table.ForeignKey(
                        name: "fk_estructuras_estructuras_padre_estructura_id",
                        column: x => x.padre_estructura_id,
                        principalTable: "estructuras",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_estructuras_sub_capitulo_sub_capitulo_id",
                        column: x => x.sub_capitulo_id,
                        principalTable: "subcapitulos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "archivos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre_archivo = table.Column<string>(type: "character varying(220)", maxLength: 220, nullable: true),
                    descripcion_archivo = table.Column<string>(type: "character varying(550)", maxLength: 550, nullable: true),
                    ruta_archivo = table.Column<string>(type: "character varying(350)", maxLength: 350, nullable: true),
                    fecha_carga = table.Column<DateOnly>(type: "date", nullable: false),
                    tipo_archivo_id = table.Column<int>(type: "integer", nullable: false),
                    sub_capitulo_id = table.Column<Guid>(type: "uuid", nullable: true),
                    estructura_id = table.Column<Guid>(type: "uuid", nullable: true),
                    estado_archivo = table.Column<int>(type: "integer", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_modificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_eliminacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_archivos", x => x.id);
                    table.ForeignKey(
                        name: "fk_archivos_estructura_estructura_id",
                        column: x => x.estructura_id,
                        principalTable: "estructuras",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_archivos_sub_capitulo_sub_capitulo_id",
                        column: x => x.sub_capitulo_id,
                        principalTable: "subcapitulos",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "planos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    escala_id = table.Column<int>(type: "integer", nullable: false),
                    sistema_proyeccion = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    nombre_plano = table.Column<string>(type: "character varying(220)", maxLength: 220, nullable: true),
                    codigo_plano = table.Column<string>(type: "text", nullable: true),
                    archivo_id = table.Column<Guid>(type: "uuid", nullable: false),
                    colaborador_id = table.Column<Guid>(type: "uuid", nullable: false),
                    estado_plano = table.Column<int>(type: "integer", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_modificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_eliminacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_planos", x => x.id);
                    table.ForeignKey(
                        name: "fk_planos_archivos_archivo_id",
                        column: x => x.archivo_id,
                        principalTable: "archivos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_planos_colaboradores_colaborador_id",
                        column: x => x.colaborador_id,
                        principalTable: "colaboradores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "plano_detalle",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    plano_id = table.Column<Guid>(type: "uuid", nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: true),
                    coordenadas = table.Column<Geometry>(type: "geometry", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_modificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_eliminacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_plano_detalle", x => x.id);
                    table.ForeignKey(
                        name: "fk_plano_detalle_plano_plano_id",
                        column: x => x.plano_id,
                        principalTable: "planos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_archivo_descripcion",
                table: "archivos",
                column: "descripcion_archivo");

            migrationBuilder.CreateIndex(
                name: "ix_archivos_estructura_id",
                table: "archivos",
                column: "estructura_id");

            migrationBuilder.CreateIndex(
                name: "ix_archivos_sub_capitulo_id",
                table: "archivos",
                column: "sub_capitulo_id");

            migrationBuilder.CreateIndex(
                name: "ix_capitulo_nombre",
                table: "capitulos",
                column: "nombre_capitulo");

            migrationBuilder.CreateIndex(
                name: "ix_capitulos_estudio_ambiental_id",
                table: "capitulos",
                column: "estudio_ambiental_id");

            migrationBuilder.CreateIndex(
                name: "ix_colaborador_generocolaboradorid",
                table: "colaboradores",
                column: "genero_colaborador_id");

            migrationBuilder.CreateIndex(
                name: "ix_colaborador_nombrecompletocolaborador",
                table: "colaboradores",
                column: "nombre_completo_colaborador");

            migrationBuilder.CreateIndex(
                name: "ix_colaborador_nombrescolaborador",
                table: "colaboradores",
                column: "nombres_colaborador");

            migrationBuilder.CreateIndex(
                name: "ix_colaborador_numerodocumentoidentidad",
                table: "colaboradores",
                column: "numero_documento_identidad");

            migrationBuilder.CreateIndex(
                name: "ix_colaboradores_consultora_id",
                table: "colaboradores",
                column: "consultora_id");

            migrationBuilder.CreateIndex(
                name: "ix_constante_clave_valor",
                table: "constantes",
                columns: new[] { "clave", "valor" });

            migrationBuilder.CreateIndex(
                name: "ix_constante_tipoconstante",
                table: "constantes",
                column: "tipo_constante");

            migrationBuilder.CreateIndex(
                name: "ix_consultora_correoorganizacional",
                table: "consultoras",
                column: "correo_organizacional");

            migrationBuilder.CreateIndex(
                name: "ix_consultora_nombreconsultora",
                table: "consultoras",
                column: "nombre_consultora");

            migrationBuilder.CreateIndex(
                name: "ix_consultora_rucconsultora",
                table: "consultoras",
                column: "ruc_consultora");

            migrationBuilder.CreateIndex(
                name: "ix_contador_entidad_agrupador",
                table: "contadores",
                columns: new[] { "entidad", "agrupador" });

            migrationBuilder.CreateIndex(
                name: "ix_empresa_departamentoempresaid",
                table: "empresas",
                column: "departamento_empresa_id");

            migrationBuilder.CreateIndex(
                name: "ix_empresa_distritoempresaid",
                table: "empresas",
                column: "distrito_empresa_id");

            migrationBuilder.CreateIndex(
                name: "ix_empresa_longitudempresa",
                table: "empresas",
                column: "longitud_empresa");

            migrationBuilder.CreateIndex(
                name: "ix_empresa_organizacionid",
                table: "empresas",
                column: "organizacion_id");

            migrationBuilder.CreateIndex(
                name: "ix_empresa_provinciaempresaid",
                table: "empresas",
                column: "provincia_empresa_id");

            migrationBuilder.CreateIndex(
                name: "ix_empresa_razonsocial",
                table: "empresas",
                column: "razon_social");

            migrationBuilder.CreateIndex(
                name: "ix_empresa_rucempresa",
                table: "empresas",
                column: "ruc_empresa");

            migrationBuilder.CreateIndex(
                name: "ix_estructura_padreestructuraid",
                table: "estructuras",
                column: "padre_estructura_id");

            migrationBuilder.CreateIndex(
                name: "ix_estructura_subcapituloid",
                table: "estructuras",
                column: "sub_capitulo_id");

            migrationBuilder.CreateIndex(
                name: "ix_estudioambiental_codigoestudio",
                table: "estudios_ambientales",
                column: "codigo_estudio");

            migrationBuilder.CreateIndex(
                name: "ix_estudioambiental_proyecto",
                table: "estudios_ambientales",
                column: "proyecto");

            migrationBuilder.CreateIndex(
                name: "ix_estudios_ambientales_empresa_id",
                table: "estudios_ambientales",
                column: "empresa_id");

            migrationBuilder.CreateIndex(
                name: "ix_menu_item_options_parent_id",
                table: "menu_item_options",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "ix_menu_label",
                table: "menu_item_options",
                column: "label");

            migrationBuilder.CreateIndex(
                name: "ix_menu_roles_role_id",
                table: "menu_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_organizacion_nombreorganizacion",
                table: "organizaciones",
                column: "nombre_organizacion");

            migrationBuilder.CreateIndex(
                name: "ix_permission_name",
                table: "permissions",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_permission_referenciacontrol",
                table: "permissions",
                column: "referencia_control");

            migrationBuilder.CreateIndex(
                name: "ix_plano_detalle_plano_id",
                table: "plano_detalle",
                column: "plano_id");

            migrationBuilder.CreateIndex(
                name: "ix_planos_archivo_id",
                table: "planos",
                column: "archivo_id");

            migrationBuilder.CreateIndex(
                name: "ix_planos_colaborador_id",
                table: "planos",
                column: "colaborador_id");

            migrationBuilder.CreateIndex(
                name: "ix_expires",
                table: "refresh_tokens",
                column: "expires");

            migrationBuilder.CreateIndex(
                name: "ix_token",
                table: "refresh_tokens",
                column: "token");

            migrationBuilder.CreateIndex(
                name: "ix_user_id",
                table: "refresh_tokens",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_rolepermissions_permissionid",
                table: "role_permissions",
                column: "permission_id");

            migrationBuilder.CreateIndex(
                name: "ix_role_name",
                table: "roles",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_subcapitulo_nombre",
                table: "subcapitulos",
                column: "nombre_sub_capitulo");

            migrationBuilder.CreateIndex(
                name: "ix_subcapitulos_capitulo_id",
                table: "subcapitulos",
                column: "capitulo_id");

            migrationBuilder.CreateIndex(
                name: "ix_ubigeo_descripcion",
                table: "ubigeos",
                column: "descripcion");

            migrationBuilder.CreateIndex(
                name: "ix_ubigeo_nivel",
                table: "ubigeos",
                column: "nivel");

            migrationBuilder.CreateIndex(
                name: "ix_ubigeo_nivel_descripcion",
                table: "ubigeos",
                columns: new[] { "nivel", "descripcion" });

            migrationBuilder.CreateIndex(
                name: "ix_ubigeos_padre_id",
                table: "ubigeos",
                column: "padre_id");

            migrationBuilder.CreateIndex(
                name: "ix_userroles_roleid",
                table: "user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_nombrecompleto",
                table: "users",
                column: "nombre_completo");

            migrationBuilder.CreateIndex(
                name: "ix_user_userdni",
                table: "users",
                column: "user_dni");

            migrationBuilder.CreateIndex(
                name: "ix_user_username",
                table: "users",
                column: "user_name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "constantes");

            migrationBuilder.DropTable(
                name: "contadores");

            migrationBuilder.DropTable(
                name: "menu_roles");

            migrationBuilder.DropTable(
                name: "plano_detalle");

            migrationBuilder.DropTable(
                name: "refresh_tokens");

            migrationBuilder.DropTable(
                name: "role_permissions");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "menu_item_options");

            migrationBuilder.DropTable(
                name: "planos");

            migrationBuilder.DropTable(
                name: "permissions");

            migrationBuilder.DropTable(
                name: "archivos");

            migrationBuilder.DropTable(
                name: "colaboradores");

            migrationBuilder.DropTable(
                name: "estructuras");

            migrationBuilder.DropTable(
                name: "consultoras");

            migrationBuilder.DropTable(
                name: "subcapitulos");

            migrationBuilder.DropTable(
                name: "capitulos");

            migrationBuilder.DropTable(
                name: "estudios_ambientales");

            migrationBuilder.DropTable(
                name: "empresas");

            migrationBuilder.DropTable(
                name: "organizaciones");

            migrationBuilder.DropTable(
                name: "ubigeos");
        }
    }
}
