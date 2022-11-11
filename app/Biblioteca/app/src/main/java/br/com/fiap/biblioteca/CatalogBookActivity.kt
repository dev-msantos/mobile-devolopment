package br.com.fiap.biblioteca

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.Toast
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import br.com.fiap.biblioteca.adapter.AdapterBook
import br.com.fiap.biblioteca.data.model.GenericResponse
import br.com.fiap.biblioteca.data.model.Livro
import br.com.fiap.biblioteca.data.model.Livros
import br.com.fiap.biblioteca.data.remote.RetrofitBuilder
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class CatalogBookActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_catalog_book)

        val btn_new_book = findViewById<Button>(R.id.btn_create_new_book)

        val recyclerviewBook = findViewById<RecyclerView>(R.id.recyclerview)
        recyclerviewBook.layoutManager = LinearLayoutManager(this, LinearLayoutManager.VERTICAL, false)
        recyclerviewBook.setHasFixedSize(true)

        val bookList: MutableList<Livro> = mutableListOf()
        val adapterBook = AdapterBook(this, bookList)

        val call = RetrofitBuilder().livroService.gelAll()

        call.enqueue(object : Callback<GenericResponse<Livros>?> {
            override fun onResponse(
                call: Call<GenericResponse<Livros>?>,
                response: Response<GenericResponse<Livros>?>
            ) {
                if (response.isSuccessful)
                {
                    response.body()?.obj?.livros?.forEach {
                        bookList.add(it)
                    }
                    adapterBook.notifyDataSetChanged()

                    Toast.makeText(applicationContext, response.body()?.responseMessage, 3000).show()
                }
            }

            override fun onFailure(call: Call<GenericResponse<Livros>?>, t: Throwable) {
                Toast.makeText(applicationContext, t.message, 3000).show()
            }
        })

        recyclerviewBook.adapter = adapterBook

        btn_new_book.setOnClickListener {
            openCreateNewBook()
        }
    }

    private fun openCreateNewBook() {
        val intent = Intent(this, BookActivity::class.java)
        startActivity(intent);
    }
}