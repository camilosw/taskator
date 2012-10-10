class CreateEstados < ActiveRecord::Migration
  def change
    create_table :estados do |t|
      t.string :nombre, null: false

      t.timestamps
    end
  end
end
