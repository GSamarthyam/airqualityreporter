package com.mycompany.fileprocessors;

import com.itextpdf.text.*;
import com.itextpdf.text.pdf.PdfPCell;
import com.itextpdf.text.pdf.PdfPTable;
import com.itextpdf.text.pdf.PdfWriter;

import java.io.FileOutputStream;
import java.io.IOException;
import java.util.List;

/* The PDFDocument class abstracts the details specific to writing PDF files;
   It uses the itext PDF report library
 */
public class PDFDocument implements AutoCloseable {
    private String fileName;
    private Document document;
    private static Font catFont = new Font(Font.FontFamily.TIMES_ROMAN, 18,
            Font.BOLD);
    private static Font smallBold = new Font(Font.FontFamily.TIMES_ROMAN, 12,
            Font.BOLD);

    public PDFDocument(String fileName) throws IOException, DocumentException {
        this.fileName = fileName;
        document = new Document();
        PdfWriter.getInstance(document, new FileOutputStream(fileName));
        document.open();
    }

    public void addTitle(String title) throws DocumentException {
        document.add(new Paragraph(title, catFont));
    }

    public void addHeader(String header) throws DocumentException {
        document.add(new Paragraph(header, smallBold));
    }

    public void addTable(String title, List<String> headers, List<String> entries)
            throws DocumentException {
        PdfPTable table = new PdfPTable(headers.size());

        headers.forEach(header -> addTableCell(table, header));
        entries.forEach(table::addCell);

        Paragraph paragraph = new Paragraph(title);
        paragraph.add(table);
        document.add(paragraph);
    }

    // Helper method to add a table cell with the given header value
    private void addTableCell(PdfPTable table, String header) {
        PdfPCell c1 = new PdfPCell(new Phrase(header));
        c1.setHorizontalAlignment(Element.ALIGN_CENTER);
        table.addCell(c1);
    }

    @Override
    public void close() throws Exception {
        document.close();
    }
}