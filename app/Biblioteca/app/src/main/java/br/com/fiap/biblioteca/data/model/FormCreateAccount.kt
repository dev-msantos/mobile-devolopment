package br.com.fiap.biblioteca.data.model

import com.squareup.moshi.Json

public class FormCreateAccount (
    @field:Json(name = "username") val username: String,
    @field:Json(name = "name") val name: String,
    @field:Json(name = "password") val password: String,
    @field:Json(name = "confirmePassword") val confirmePassword: String
) {
    override fun toString(): String {
        return "username: $username name: $name password: $password confirmePassword: $confirmePassword"
    }
}