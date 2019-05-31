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


    // lista todas as usuarios
    listarTodosUsuarios() {
        fetch('http://localhost:5000/api/Usuarios', {
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
                <Table striped bordered hover size="sm">
                    
                    <tbody> 
                        <tr>
                            <th>ID do Usuário</th>
                            <th>ID Tipo de Usuário</th>
                            <th>Email</th>
                            <th>Senha</th>
                           
                        </tr>

                        {
                            this.state.usuarios.map(usuario => {
                                return (
                                    <tr key={usuario.idUsuario}>
                                        <td>{usuario.idUsuario}</td>
                                        <td>{usuario.idTipoUsuario}</td>
                                        <td>{usuario.email}</td>
                                        <td>{usuario.senha}</td>
                                       
                                    </tr>
                                );
                            })
                        }
                    </tbody>
                </Table>
                <Link onClick={logout}>Sair</Link>
            </div>
        );
    }
}


export default ListagemUsuarios;