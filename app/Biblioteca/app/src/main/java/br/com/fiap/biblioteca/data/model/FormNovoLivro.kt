package br.com.fiap.biblioteca.data.model;

import com.squareup.moshi.Json

public class FormNovoLivro (
    @field:Json(name = "autor") val autor: String,
    @field:Json(name = "titulo") val titulo: String,
    @field:Json(name = "ano") val ano: Int
)
