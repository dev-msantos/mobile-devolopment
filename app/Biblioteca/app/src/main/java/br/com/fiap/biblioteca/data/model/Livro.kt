package br.com.fiap.biblioteca.data.model

import com.squareup.moshi.Json

data class Livro (
    @field:Json(name = "id") val id: String,
    @field:Json(name = "autor") val autor: String,
    @field:Json(name = "titulo") val titulo: String,
    @field:Json(name = "ano") val ano: Int
)
{
    override fun toString(): String {
        return "id: $id autor: $autor titulo: $titulo ano: $ano"
    }
}

