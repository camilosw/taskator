class Tarea < ActiveRecord::Base
  belongs_to :proyecto
  belongs_to :estado
  belongs_to :user
  
  attr_accessible :descripcion
end
