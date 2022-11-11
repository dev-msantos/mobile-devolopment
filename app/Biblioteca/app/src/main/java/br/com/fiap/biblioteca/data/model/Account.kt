package br.com.fiap.biblioteca.data.model

import com.squareup.moshi.Json

data class Account (
    @field:Json(name = "id") val id: String,
    @field:Json(name = "username") val username: String,
    @field:Json(name = "name") val name: String
) {
    override fun toString(): String {
        return "id: $id userName: $username name: $name"
    }
}