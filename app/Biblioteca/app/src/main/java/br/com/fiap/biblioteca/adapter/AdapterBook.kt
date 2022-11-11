package br.com.fiap.biblioteca.adapter

import android.content.Context
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.EditText
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import br.com.fiap.biblioteca.R
import br.com.fiap.biblioteca.data.model.Livro

class AdapterBook(private val context: Context, private val books: MutableList<Livro>): RecyclerView.Adapter<AdapterBook.BookViewHolder>() {

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): BookViewHolder {
        val itemLista = LayoutInflater.from(context).inflate(R.layout.book_item, parent, false)
        val holder  = BookViewHolder(itemLista)
        return holder
    }

    override fun onBindViewHolder(holder: BookViewHolder, position: Int) {
        holder.bookId.text = "Id: " + books[position].id
        holder.actor.text = "Actor: " + books[position].autor
        holder.title.text = "Title: " + books[position].titulo
        holder.year.text = "Year: " + books[position].ano.toString()
    }

    override fun getItemCount(): Int = books.size

    inner class BookViewHolder(itemView: View): RecyclerView.ViewHolder(itemView) {
        val bookId = itemView.findViewById<TextView>(R.id.book_id)
        val actor = itemView.findViewById<TextView>(R.id.actor)
        val title = itemView.findViewById<TextView>(R.id.title)
        val year = itemView.findViewById<TextView>(R.id.year)
    }
}