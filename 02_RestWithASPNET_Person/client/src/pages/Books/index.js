import React, { useState, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import { FiPower, FiEdit, FiTrash2 } from "react-icons/fi";

import api from "../../services/api";

import "./styles.css";

import logoImage from "../../assets/logo.svg";

export default function Books() {
    const navigate = useNavigate();
    const [books, setBooks] = useState([]);
    const [page, setPage] = useState(1);
    const [pagesCount, setPagesCount] = useState(0);

    const userName = localStorage.getItem("userName");

    const accessToken = localStorage.getItem("accessToken");

    const authorizationHeader = {
        headers: {
            Authorization: `Bearer ${accessToken}`,
        },
    };

    useEffect(() => {
        fetchPaginationBooks(true).then(() => {
            alert('Loaded!');
        },
        (error) => {
            alert(`Error: ${error}`);
        })
    }, [accessToken]);

    async function fetchPaginationBooks(isNext) {
        if (isNext)
            setPage(page + 1);
        else if (!isNext && page > 1)
            setPage(page - 1)
        
        const response = await api.get(
            `api/Book/v1/asc/4/${page}`,
            authorizationHeader
        );


        // scrolling
        setBooks([...books, ...response.data.list]);
        setPage(page + 1);

        // without scrolling
        // setBooks(response.data.list);

        setPagesCount(Math.floor(response.data.totalResults / response.data.pageSize));
    }

    // Logout Method
    async function logout() {
        try {
            await api.get("api/auth/v1/revoke", authorizationHeader);

            localStorage.clear();
            navigate("/");
        } catch (error) {
            alert("Logout failed! Try again!");
        }
    }

    // Edit Book Method
    async function editBook(id) {
        try {
            navigate(`/book/new/${id}`);
        } catch (error) {
            alert("Edit book failed! Try again!");
        }
    }

    // Delete Book Method
    async function deleteBook(id) {
        try {
            await api.delete(`api/Book/v1/${id}`, authorizationHeader);

            setBooks(books.filter((book) => book.id !== id));
        } catch (error) {
            alert("Delete failed! Try again!");
        }
    }

    return (
        <div className="book-container">
            <header>
                <img src={logoImage} alt="DSchaly" />
                <span>
                    Welcome, <strong>{userName.toUpperCase()}</strong>!
                </span>
                <Link className="button" to="/book/new/0">
                    Add New Book
                </Link>
                <button onClick={logout} type="button">
                    <FiPower size={18} color="#251FC5" />
                </button>
            </header>

            <h1>Registered Books</h1>
            <div className="pagination">
                {/* <button className="pagination-button" disabled={page === 1} onClick={() => fetchPaginationBooks(false)}>
                    Load Previous
                </button> */}

                <button className="pagination-button" disabled={page === pagesCount} onClick={() => fetchPaginationBooks(true)}>
                    Load More
                </button>
            </div>
            <ul>
                {books.map((book) => (
                    <li key={book.id}>
                        <strong>Title:</strong>
                        <p>{book.title}</p>
                        <strong>Author:</strong>
                        <p>{book.author}</p>
                        <strong>Price:</strong>
                        <p>
                            {Intl.NumberFormat("pt-BR", {
                                style: "currency",
                                currency: "BRL",
                            }).format(book.price)}
                        </p>
                        <strong>Release Date:</strong>
                        <p>
                            {Intl.DateTimeFormat("pt-BR").format(
                                new Date(book.launchDate)
                            )}
                        </p>

                        <button onClick={() => editBook(book.id)} type="button">
                            <FiEdit size={20} color="#251FC5"></FiEdit>
                        </button>

                        <button
                            onClick={() => deleteBook(book.id)}
                            type="button"
                        >
                            <FiTrash2 size={20} color="#251FC5"></FiTrash2>
                        </button>
                    </li>
                ))}
            </ul>
        </div>
    );
}
