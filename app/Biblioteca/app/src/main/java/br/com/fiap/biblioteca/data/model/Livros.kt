package br.com.fiap.biblioteca.data.model

import com.squareup.moshi.Json

data class Livros(
    @field:Json(name = "livros") val livros: List<Livro>
) {
}