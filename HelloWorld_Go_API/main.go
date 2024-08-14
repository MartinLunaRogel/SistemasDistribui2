package main

import (
	"fmt"
	"net/http"
)

func handler(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "text/plain")
	fmt.Fprint(w, "Martin Luna Rogel - Go")
}

func main() {
	http.HandleFunc("/health", handler)
	fmt.Println("Starting server on: 8080")
	if err := http.ListenAndServe(":8080", nil); err != nil {
		fmt.Println("Error starting server: ", err)
	}
}
