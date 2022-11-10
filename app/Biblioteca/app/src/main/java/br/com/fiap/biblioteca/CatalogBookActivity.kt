package br.com.fiap.biblioteca

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button

class CatalogBookActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_catalog_book)

        val btn_new_book = findViewById<Button>(R.id.btn_create_new_book)

        btn_new_book.setOnClickListener{
            openCreateNewBook()
        }




    }

    private fun openCreateNewBook() {
        val intent = Intent(this, BookActivity::class.java)
        startActivity(intent);
    }
}