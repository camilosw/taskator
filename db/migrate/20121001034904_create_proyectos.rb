class CreateProyectos < ActiveRecord::Migration
  def change
    create_table :proyectos do |t|
      t.references :cliente
      t.string :nombre, null: false

      t.timestamps
    end
    add_index :proyectos, :cliente_id
  end
end
