using NexaSoft.Agro.Application.Features.Proyectos.EstudiosAmbientales.Queries.GetEstudiosReport;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using static NexaSoft.Agro.Domain.Features.Proyectos.EstudiosAmbientales.Dtos.EstudioAmbientalDto;

namespace NexaSoft.Agro.Infrastructure.Repositories.Reports;

public class StudyTreePdfReportGenerator : IStudyTreePdfReportGenerator
{
    public byte[] GenerateStudyTreeReport(EstudioAmbientalDtoResponse study, string title = "Reporte de Estudio", string subtitle = "", string logoPath = "")
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(20);
                page.Size(PageSizes.A4);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(10).FontFamily("Helvetica"));

                // Encabezado
                page.Header().Height(80).Background(Colors.White).Row(row =>
                {
                    if (!string.IsNullOrEmpty(logoPath))
                    {
                        row.ConstantItem(50).Height(50).Image(logoPath);
                    }

                    row.RelativeItem().PaddingLeft(10).Column(column =>
                    {
                        column.Item().Text(title).FontSize(18).Bold().FontColor("#1a3a5f");
                        column.Item().Text(study.Proyecto).FontSize(14).SemiBold().FontColor("#2c5c8a");

                        if (!string.IsNullOrEmpty(subtitle))
                        {
                            column.Item().PaddingTop(2).Text(subtitle).FontSize(10).FontColor(Colors.Grey.Darken1);
                        }
                    });

                    row.ConstantItem(80).AlignRight().Text(t =>
                    {
                        t.Span("Pág. ").FontSize(9);
                        t.CurrentPageNumber().FontSize(9).Bold();
                        t.Span(" de ").FontSize(9);
                        t.TotalPages().FontSize(9).Bold();
                    });
                });

                // Contenido principal
                page.Content().PaddingTop(10).Column(column =>
                {
                    // Información general del estudio
                    column.Item().Background(Colors.White).Border(1).BorderColor("#e0e0e0").Padding(15).Column(studyColumn =>
                    {
                        studyColumn.Item().Text("INFORMACIÓN GENERAL DEL ESTUDIO").FontSize(12).Bold().FontColor("#1a3a5f");
                        studyColumn.Item().PaddingBottom(5).LineHorizontal(1).LineColor("#e8e8e8");

                        studyColumn.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(120);
                                columns.RelativeColumn();
                            });

                            AddTableRow(table, "Proyecto:", study.Proyecto);
                            AddTableRow(table, "Fecha Inicio:", study.FechaInicio.ToString("dd/MM/yyyy"));
                            AddTableRow(table, "Fecha Fin:", study.FechaFin.ToString("dd/MM/yyyy"));
                            AddTableRow(table, "Detalles:", study.Detalles!);
                            AddTableRow(table, "Fecha Creación:", study.FechaCreacionEstudio.ToString("dd/MM/yyyy HH:mm"));
                        });
                    });

                    column.Item().Height(15);

                    // Capítulos recursivos
                    if (study.Capitulos != null && study.Capitulos.Any())
                    {
                        foreach (var capitulo in study.Capitulos.OrderBy(c => c.NombreCapitulo))
                        {
                            RenderCapitulo(column, capitulo, 0);
                        }
                    }
                    else
                    {
                        column.Item().Padding(10).AlignCenter().Text("No hay capítulos definidos")
                            .FontSize(10).Italic().FontColor(Colors.Grey.Medium);
                    }
                });

                // Pie de página
                page.Footer().Height(25).Background(Colors.White).AlignCenter().Text(text =>
                {
                    text.Span("© ").FontSize(8).FontColor(Colors.Grey.Medium);
                    text.Span(DateTime.Now.Year.ToString()).FontSize(8).Bold().FontColor(Colors.Grey.Darken1);
                    text.Span(" NexaSoft Agro | ").FontSize(8).FontColor(Colors.Grey.Medium);
                    text.Span("Confidencial").FontSize(8).Italic().FontColor(Colors.Grey.Medium);
                });
            });
        });

        return document.GeneratePdf();
    }

    private void RenderCapitulo(ColumnDescriptor parent, CapituloDtoResponse capitulo, int level)
    {
        var paddingLeft = level * 15;

        parent.Item().Element(container =>
            container
                .Background(level == 0 ? Colors.White : Colors.Grey.Lighten3)
                .Border(1)
                .BorderColor(level == 0 ? "#e0e0e0" : "#d0d0d0")
                .Padding(10)
                .PaddingLeft(10 + paddingLeft)
                .Column(capituloColumn =>
                {
                    capituloColumn.Item().Row(row =>
                    {
                        row.RelativeItem().Text($"CAPÍTULO: {capitulo.NombreCapitulo}")
                            .FontSize(level == 0 ? 12 : 11)
                            .Bold()
                            .FontColor(level == 0 ? "#1a3a5f" : "#2c5c8a");
                    });

                    if (!string.IsNullOrEmpty(capitulo.DescripcionCapitulo))
                    {
                        capituloColumn.Item().PaddingTop(2).Text(capitulo.DescripcionCapitulo)
                            .FontSize(10).FontColor(Colors.Grey.Darken2);
                    }

                    // Subcapítulos
                    if (capitulo.SubCapitulos != null && capitulo.SubCapitulos.Any())
                    {
                        capituloColumn.Item().PaddingTop(5);

                        foreach (var subCapitulo in capitulo.SubCapitulos.OrderBy(sc => sc.NombreSubCapitulo))
                        {
                            RenderSubCapitulo(capituloColumn, subCapitulo, level + 1);
                        }
                    }
                })
        );
    }

    private void RenderSubCapitulo(ColumnDescriptor parent, SubCapituloDtoResponse subCapitulo, int level)
    {
        var paddingLeft = level * 15;

        parent.Item().Element(container =>
            container
                .Background(Colors.Grey.Lighten4)
                .Border(1).BorderColor("#d8d8d8")
                .Padding(8).PaddingLeft(8 + paddingLeft)
                .Column(subCapituloColumn =>
                {
                    subCapituloColumn.Item().Text($"SUBCAPÍTULO: {subCapitulo.NombreSubCapitulo}")
                        .FontSize(11).SemiBold().FontColor("#3a6ea5");

                    if (!string.IsNullOrEmpty(subCapitulo.DescripcionSubCapitulo))
                    {
                        subCapituloColumn.Item().PaddingTop(2).Text(subCapitulo.DescripcionSubCapitulo)
                            .FontSize(9).FontColor(Colors.Grey.Darken1);
                    }

                    // Estructuras (recursivo)
                    if (subCapitulo.Estructuras?.Any() == true)
                    {
                        var estructurasRaiz = subCapitulo.Estructuras
                            .Where(e => e.PadreEstructuraId == null)
                            .OrderBy(e => e.DescripcionEstructura)
                            .ToList();

                        foreach (var estructura in estructurasRaiz)
                        {
                            RenderEstructura(subCapituloColumn, estructura, subCapitulo.Estructuras.ToList(), level + 1);
                        }
                    }

                    // Archivos del subcapítulo
                    if (subCapitulo.Archivos?.Any() == true)
                    {
                        RenderArchivosSection(subCapituloColumn, "Archivos del Subcapítulo", subCapitulo.Archivos.ToList(), level + 1);
                    }
                })
        );
    }


    private void RenderEstructura(ColumnDescriptor parent, EstructuraDtoResponse estructura, List<EstructuraDtoResponse> todasEstructuras, int level)
    {
        var paddingLeft = level * 15;
        var backgroundColor = level % 2 == 0 ? Colors.White : Colors.Grey.Lighten4;

        parent.Item().Element(container =>
            container
                .Background(backgroundColor)
                .Border(1).BorderColor("#e0e0e0")
                .Padding(6).PaddingLeft(6 + paddingLeft)
                .Column(estructuraColumn =>
                {
                    estructuraColumn.Item().Text($"{GetTipoEstructuraText(estructura.Hijos != null ? 1 : 2)}: {estructura.NombreEstructura}")
                        .FontSize(10).SemiBold().FontColor(level == 1 ? "#4a7bab" : "#5a8bbb");

                    if (!string.IsNullOrEmpty(estructura.DescripcionEstructura))
                    {
                        estructuraColumn.Item().PaddingTop(2).Text(estructura.DescripcionEstructura)
                            .FontSize(9).FontColor(Colors.Grey.Darken1);
                    }


                    // Archivos de la estructura
                    if (estructura.Archivos?.Any() == true)
                    {
                        RenderArchivosSection(estructuraColumn, "Archivos adjuntos", estructura.Archivos.ToList(), level + 1);
                    }


                    if (estructura.Hijos != null && estructura.Hijos.Any())
                    {
                        estructuraColumn.Item().PaddingTop(3);

                        foreach (var hijo in estructura.Hijos)
                        {
                            RenderEstructura(estructuraColumn, hijo, todasEstructuras, level + 1);
                        }
                    }
                })
        );
    }

    private void RenderArchivosSection(ColumnDescriptor parent, string title, List<ArchivoDtoResponse> archivos, int level)
    {
        var paddingLeft = level * 12;

        parent.Item().Element(container =>
            container
                .PaddingTop(3)
                .PaddingLeft(paddingLeft)
                .Column(archivosColumn =>
                {
                    archivosColumn.Item().Text(title)
                        .FontSize(9)
                        .Bold()
                        .FontColor(Colors.Grey.Darken2);

                    foreach (var archivo in archivos.OrderBy(a => a.Nombre))
                    {
                        archivosColumn.Item().PaddingLeft(5).Row(row =>
                        {
                            row.ConstantItem(20).Text("•").FontSize(9);

                            row.RelativeItem().Text($"{archivo.Nombre} - {archivo.DescripcionArchivo}")
                                .FontSize(9)
                                .FontColor(Colors.Grey.Darken1);

                            if (archivo.Plano != null)
                            {
                                row.AutoItem()
                                    .Background("#1a3a5f")
                                    .PaddingHorizontal(4)
                                    .PaddingVertical(1)
                                    .Text("PLANO")
                                    .FontSize(7)
                                    .FontColor(Colors.White);
                            }
                        });
                    }
                })
        );
    }


    private string GetTipoEstructuraText(int tipoEstructura)
    {
        return tipoEstructura switch
        {
            1 => "Estructura",
            2 => "Sub-Estructura",
            _ => "Item"
        };
    }

    private void AddTableRow(TableDescriptor table, string label, string value)
    {
        table.Cell().PaddingBottom(3).Text(label).FontSize(10).SemiBold().FontColor(Colors.Grey.Darken2);
        table.Cell().PaddingBottom(3).Text(value).FontSize(10);
    }
}
