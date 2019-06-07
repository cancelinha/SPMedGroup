import React, { Component } from "react";
import { Link } from "react-router-dom";
import { logout } from "../../services/logout";
import "../../assets/css/List_Users.css"
import "../../../node_modules/bootstrap/dist/css/bootstrap.css"
import Table from "../../../node_modules/react-bootstrap/Table"

class ListagemUsuarios extends Component {
    constructor() {
        super();
        this.state = {
            usuarios: [],

        }

    }

    logout() {
        localStorage.removeItem("userautent-token-spmedicalgroup");
    }

    // lista todas as usuarios
    listarTodosUsuarios() {
        fetch('http://192.168.3.215:5000/api/Usuarios', {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                "Authorization": 'Bearer ' + localStorage.getItem("userautent-token-spmedicalgroup")
            }
        })
            .then(resposta => resposta.json())
            .then(data => this.setState({ usuarios: data }))
            // .then(data => {
            //     if (data.status == 200) {
            //         this.setState({ usuarios: data })
            //     }
            // })
            .catch(erro => console.log(erro))
    }


    // carrega o metodo
    componentDidMount() {
        this.listarTodosUsuarios();

    }


    render() {
        return (
            <div className="all">
                <h1>Lista Atualizada dos Usuários:</h1>
                <div class="table">
                    <Table striped bordered hover size="sm">

                        <tbody>
                            <tr>
                                <th>ID do Usuário</th>
                                <th>Tipo Usuário</th>
                                <th>Email</th>
                                <th>Senha</th>

                            </tr>

                            {
                                this.state.usuarios.map(usuario => {
                                    return (
                                        <tr key={usuario.idUsuario}>
                                            <td>{usuario.idUsuario}</td>
                                            <td>{usuario.idTipoUsuarioNavigation.nome}</td>
                                            <td>{usuario.email}</td>
                                            <td>{usuario.senha}</td>

                                        </tr>
                                    );
                                })
                            }
                        </tbody>
                    </Table>
                    <Link to="/">
                        <button onClick={this.logout}>Sair</button>
                    </Link>
                </div>
            </div>
        );
    }
}


export default ListagemUsuarios;