package br.com.fiap.biblioteca.data.remote

import br.com.fiap.biblioteca.data.model.FormNovoLivro
import br.com.fiap.biblioteca.data.model.GenericResponse
import br.com.fiap.biblioteca.data.model.Livro
import retrofit2.Call
import retrofit2.http.*

public interface LivroService {

    @Headers("Content-Type: application/json")
    @POST(value="Livro")
    fun novoLivro(@Body form: FormNovoLivro): Call<GenericResponse<Livro>>

    @Headers("Content-Type: application/json")
    @GET(value="Livro/{id}")
    fun getById(@Path("id") id: String): Call<GenericResponse<Livro>>

    @Headers("Content-Type: application/json")
    @GET(value="Livro")
    fun gelAll(): Call<GenericResponse<List<Livro>>>

    @Headers("Content-Type: application/json")
    @PUT(value="Livro")
    fun updateBook(@Body livro: Livro): Call<GenericResponse<Livro>>

    @Headers("Content-Type: application/json")
    @DELETE(value="Livro/{id}")
    fun deleteBook(@Path("id") id: String): Call<GenericResponse<Boolean>>
}