class Tarea < ActiveRecord::Base
  belongs_to :proyecto
  belongs_to :estado
  belongs_to :user
  
  attr_accessible :descripcion
end

# == Schema Information
#
# Table name: tareas
#
#  id          :integer          not null, primary key
#  proyecto_id :integer
#  estado_id   :integer
#  user_id     :integer
#  descripcion :text
#  created_at  :datetime         not null
#  updated_at  :datetime         not null
#

