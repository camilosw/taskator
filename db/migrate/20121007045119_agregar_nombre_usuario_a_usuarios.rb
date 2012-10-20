class AgregarNombreUsuarioAUsuarios < ActiveRecord::Migration
  def change
    add_column :users, :nombre_usuario, :string, null: false, default: ""
    add_index :users, :nombre_usuario, unique: true
  end
end
