package br.com.fiap.biblioteca.data.remote

import br.com.fiap.biblioteca.data.model.Account
import br.com.fiap.biblioteca.data.model.FormCreateAccount
import br.com.fiap.biblioteca.data.model.FormLogin
import br.com.fiap.biblioteca.data.model.GenericResponse
import retrofit2.Call
import retrofit2.http.Body
import retrofit2.http.Headers
import retrofit2.http.POST

public interface AuthService {

    @Headers("Content-Type: application/json")
    @POST(value="Auth")
    fun login(@Body form: FormLogin): Call<GenericResponse<Account>>

    @Headers("Content-Type: application/json")
    @POST(value="Auth/create-account")
    fun createAccount(@Body form: FormCreateAccount): Call<GenericResponse<Account>>
}