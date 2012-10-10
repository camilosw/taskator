class UsuariosController < ApplicationController
  before_filter :authenticate_user!

  def index
    @usuarios = User.order(:nombre_usuario)
  end

  def show
    @usuario = User.find(params[:id])
  end

  def new
    @usuario = User.new()    
  end

  def edit
    @usuario = User.find(params[:id])
  end

  def create
    @usuario = User.new(params[:usuario])
    if @usuario.save
      redirect_to usuario_path(@usuario), notice: 'El usuario ha sido creado'
    else
      render action: "new"
    end
  end

  def update
    @usuario = User.find(params[:id])
    if @usuario.update_attributes(params[:usuario])
      redirect_to usuario_path(@usuario), notice: "El usuario ha sido editado"
    else
      render action: "edit"
    end
  end

  def destroy
    @usuario = User.find(params[:id])
    @usuario.destroy
    redirect_to usuarios_url
  end
end
