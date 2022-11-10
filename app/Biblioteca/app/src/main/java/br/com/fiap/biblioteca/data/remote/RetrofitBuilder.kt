package br.com.fiap.biblioteca.data.remote

import retrofit2.Retrofit
import retrofit2.converter.moshi.MoshiConverterFactory

class RetrofitBuilder {

    private val retrofit: Retrofit = Retrofit.Builder()
        .baseUrl("http://192.168.1.112/api/")
        .addConverterFactory(MoshiConverterFactory.create())
        .build();

    val authService = retrofit.create(AuthService::class.java)
    val livroService = retrofit.create(LivroService::class.java)
}