import React, { Component } from "react";
import { Link } from "react-router-dom";
import { logout } from "../../services/logout";

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
            <div>
                <table>
                    <tbody> 
                        <tr>
                            <th>Id</th>
                            <th>IdTipoUsuario</th>
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
                </table>
                <Link onClick={logout}>Sair</Link>
            </div>
        );
    }
}


export default ListagemUsuarios;