package br.com.fiap.biblioteca.data.model

import com.squareup.moshi.Json

class GenericResponse<T>(
    @field:Json(name = "responseMessage") val responseMessage: String?,
    @field:Json(name = "dateAndTime") val dateAndTime: String?,
    @field:Json(name = "errorList") val errorList: Array<String>?,
    @field:Json(name = "object") val obj: T
) {
    override fun toString(): String {
        return "message: $responseMessage dateAndTime: $dateAndTime errors: $errorList obj: $obj"
    }
}