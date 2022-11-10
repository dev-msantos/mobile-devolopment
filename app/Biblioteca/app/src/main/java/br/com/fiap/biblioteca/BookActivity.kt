package br.com.fiap.biblioteca

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import br.com.fiap.biblioteca.data.model.FormNovoLivro
import br.com.fiap.biblioteca.data.model.GenericResponse
import br.com.fiap.biblioteca.data.model.Livro
import br.com.fiap.biblioteca.data.remote.RetrofitBuilder
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class BookActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_book)

        val btnCreateNewBook = findViewById<Button>(R.id.btn_create_new_book)

        btnCreateNewBook.setOnClickListener{
            val campoActor = findViewById<EditText>(R.id.actor);
            val campoTitle = findViewById<EditText>(R.id.title);
            val campoYear = findViewById<EditText>(R.id.year);

            val actor = campoActor.text.toString().trim();
            val title = campoTitle.text.toString().trim();
            val year = campoYear.text.toString().toInt();

            val form = FormNovoLivro(actor, title, year)
            val call = RetrofitBuilder().livroService.novoLivro(form);

            if(actor.isNotEmpty() && title.isNotEmpty() && year.toString().isNotEmpty())
            {
                call.enqueue(object : Callback<GenericResponse<Livro>?> {
                    override fun onResponse(
                        call: Call<GenericResponse<Livro>?>,
                        response: Response<GenericResponse<Livro>?>
                    ) {
                        if (response.code().equals(201) || response.code().equals(200))
                        {
                            Toast.makeText(applicationContext, response.body()?.responseMessage, 3000).show()
                            openCatalogBook()
                        } else {
                            Toast.makeText(applicationContext, response.body()?.responseMessage, 3000).show()
                        }
                    }

                    override fun onFailure(call: Call<GenericResponse<Livro>?>, t: Throwable) {
                        Toast.makeText(applicationContext, t.message, 3000).show()
                    }
                })
            }
            else {
                Toast.makeText(applicationContext, "Autor, Título e Ano são obrigatórios", 3000).show()
            }

        }
    }

    private fun openCatalogBook() {
        val intent = Intent(this, CatalogBookActivity::class.java)
        startActivity(intent);
    }
}