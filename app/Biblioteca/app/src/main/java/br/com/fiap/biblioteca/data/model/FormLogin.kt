package br.com.fiap.biblioteca.data.model

import com.squareup.moshi.Json

public class FormLogin (
    @field:Json(name = "userName") val userName: String,
    @field:Json(name = "password") val password: String
)
